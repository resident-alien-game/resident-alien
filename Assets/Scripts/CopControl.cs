using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopControl : MonoBehaviour
{
    private FormChange formChange;
    private FieldOfView fieldOfView;
    private bool alreadyAttacked;
    private GameObject bullet;
    private GunControl gunControl;
    Animator anim;
    public GameObject gun;
    public GameObject alien;
    public UnityEngine.AI.NavMeshAgent agent;
    public float walkRange; //radius of sphere
    public float patrolSpeed = 3.0f;
    public float chaseSpeed = 4.0f;
    public float attackRange = 15.0f;
    public float reloadTime = 2.0f; // time to reload a gun (time between attacks)
    public bool shouldChase = false;
    public bool deathChase = false;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        formChange = alien.GetComponent<FormChange>();
        alreadyAttacked = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gunControl = gun.GetComponent<GunControl>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (fieldOfView.canSeePlayer && formChange.isAlien)
        {
            if (Vector3.Distance(transform.position, alien.transform.position) <= attackRange && formChange.isAlien)
            {
                Attack();
            }
            else
            {
                Chase();
            }
        }
        else if (shouldChase && !deathChase)
        {
            Chase();
        }
        else if (deathChase && fieldOfView.canSeePlayer)
        {
            if (Vector3.Distance(transform.position, alien.transform.position) <= attackRange)
            {
                Attack();
            }
            else
            {
                chaseSpeed = 6.0f;
                Chase();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        agent.speed = patrolSpeed;
        anim.SetBool("isWalking", true);
        anim.SetBool("isRuning", false);
        anim.SetBool("isShooting", false);

        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, walkRange, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }

    private void Chase()
    {
        anim.SetBool("isRuning", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isShooting", false);

        agent.SetDestination(alien.transform.position);
        agent.speed = chaseSpeed;
    }

    private void Attack()
    {
        // Stop moving when attacking
        anim.SetBool("isWalking", false);
        anim.SetBool("isRuning", false);
        anim.SetBool("isShooting", true);

        agent.SetDestination(transform.position);
        transform.LookAt(alien.transform);

        if (!alreadyAttacked)
        {
            gunControl.Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), reloadTime);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
