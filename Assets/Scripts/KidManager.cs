using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidManager : MonoBehaviour
{

    private FieldOfView fieldOfView;
    private FormChange formChange;
    [SerializeField]
    private GameObject alien;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        formChange = alien.GetComponent<FormChange>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if alien is being seen by a kid. If so, set alien as discovered.
        if (fieldOfView.canSeePlayer)
            formChange.Discovered();
    }
}
