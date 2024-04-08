using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianControl : MonoBehaviour
{
    public float x = 0;
    public float z = 0;
    float dirX = 2;
    float dirZ = 2;
    bool isMoving = true;


    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
        z = gameObject.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {

            if ((x > 3) || (x < -8))
            {
                dirX = -dirX;              
            }


            if ((z > -6) || (z < -16))
            {
                dirZ = -dirZ;
            }
            x += dirX * Time.deltaTime;
            z += dirZ * Time.deltaTime;
            Vector3 pos = new Vector3();
            pos.x = x;
            pos.y = 1;
            pos.z = z;
            gameObject.transform.position = pos;
        }
    }

    public void GotKilled()
    {
        isMoving = false;
    }
}
