using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider){
        if (!collider.CompareTag("Player")) return;
        collider.transform.parent = transform;

    }
    void OnTriggerExit(Collider collider){
        if (!collider.CompareTag("Player")) return;
        collider.transform.parent = null;
        
    }
}
