using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianControl : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private RandomMovement randomMovement;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomMovement = GetComponent<RandomMovement>();
    }

    public void GotKilled()
    {
        navMeshAgent.enabled = false;
        randomMovement.enabled = false;
    }

    public bool isDead(){
        return !isMoving;
    }
}
