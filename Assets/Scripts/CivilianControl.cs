using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private RandomMovement randomMovement;
    public bool isDead = false;
    public GameObject assignedHouse;
    public NavMeshAgent agent;
    public float range = 2; //radius of sphere
    private Vector3 targetPoint;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomMovement = GetComponent<RandomMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (assignedHouse != null)
        {
            targetPoint = GetRandomPoint(assignedHouse.transform.position, range);
            agent.SetDestination(targetPoint);
        }
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            targetPoint = GetRandomPoint(assignedHouse.transform.position, range);
            agent.SetDestination(targetPoint);
        }
    }

    public void GotKilled()
    {
        //navMeshAgent.enabled = false;
        //randomMovement.enabled = false;
        navMeshAgent.velocity = Vector3.zero;
        agent.isStopped = true;
        isDead = true;
    }

    private Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas); //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        return hit.position;
    }
}
