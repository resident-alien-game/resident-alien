using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interfaces;

public class CivilianControl : MonoBehaviour, IAttackable
{
    public SoundManagement soundManagement;
    private NavMeshAgent navMeshAgent;
    //private RandomMovement randomMovement;
    public bool isDead = false;
    public GameObject assignedHouse;
    public NavMeshAgent agent;
    public float range = 10; //radius of sphere
    private Vector3 targetPoint;
    private List<CopControl> copControls;
    Animator anim;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //randomMovement = GetComponent<RandomMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (assignedHouse != null)
        {
            targetPoint = GetRandomPoint(assignedHouse.transform.position, range);
            agent.SetDestination(targetPoint);
        }
        copControls = new List<CopControl>(FindObjectsOfType<CopControl>());
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead)
        {
            SetCopDeathChase(true);
            return;
        }
        if(!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            targetPoint = GetRandomPoint(assignedHouse.transform.position, range);
            agent.SetDestination(targetPoint);
            anim.SetBool("isWalking", true);
        }
    }

    public void GotKilled()
    {
        //navMeshAgent.enabled = false;
        //randomMovement.enabled = false;
        soundManagement.manDie();
        navMeshAgent.velocity = Vector3.zero;
        agent.isStopped = true;
        isDead = true;
        anim.SetBool("isDead", true);
    }

    public void Attack()
    {
        GotKilled();
    }

    private Vector3 GetRandomPoint(Vector3 center, float range)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas); //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        return hit.position;
    }

    private void SetCopDeathChase(bool deathChase)
    {
        foreach (CopControl copControl in copControls)
        {
            copControl.deathChase = deathChase;
        }
    }
}
