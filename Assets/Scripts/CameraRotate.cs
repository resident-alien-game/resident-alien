using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivity = -1f;
    private Vector3 rotation;
    private bool rotationLock = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rotationLock = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rotationLock = false;
        }
        if (rotationLock)
        {
            y = Input.GetAxis("Mouse X");
            x = Input.GetAxis("Mouse Y");
            rotation = new Vector3(x, y * sensitivity, 0f);
            transform.eulerAngles -= rotation;
        }
        
    }
}
