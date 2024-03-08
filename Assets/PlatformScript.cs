using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    float x = 0;
    float dirX = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((x > 2) || (x < -2)){
            dirX=-dirX;
        }
        x += dirX*Time.deltaTime;
        Vector3 pos = new Vector3();
        pos.x = x;
        gameObject.transform.position = pos;
    }
}
