using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopControl : MonoBehaviour
{
    private FormChange formChange;
    private FieldOfView fieldOfView;
    [SerializeField]
    private GameObject alien;
    public UnityEngine.AI.NavMeshAgent agent;
    public float range; //radius of sphere
    private float speed; //speed of cop
    public bool shouldChase = false;
    public bool deathChase = false;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        formChange = alien.GetComponent<FormChange>();
        //position = gameObject.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (fieldOfView.canSeePlayer && formChange.isAlien)
        {
            speed = 7;
            Chase();
        }
        else if (shouldChase && !deathChase){
            speed = 4;
            Chase();
        }
        else if (deathChase)
        {
            speed = 5;
            Chase();
        }
        else if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }

    private void Chase()
    {
        transform.LookAt(alien.transform);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
