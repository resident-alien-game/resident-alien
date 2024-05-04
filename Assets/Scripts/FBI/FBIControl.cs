using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FBIControl : MonoBehaviour
{
    public float range; //radius of sphere where random point can be selected
    public GameObject statusManager;

    private StatusManagement status;
    private NavMeshAgent agent;
    private FBIFieldOfView fov;
    private bool collectingPiece;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FBIFieldOfView>();
        status = statusManager.GetComponent<StatusManagement>();
        collectingPiece = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (fov.targetTransform && !collectingPiece)
        {
            CollectPiece();
        }
        else if (agent.remainingDistance <= agent.stoppingDistance) //done with path, so go to a new random direction.
        {
            anim.SetBool("isWalking", true);
            Vector3 point;
            if (RandomPoint(transform.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
            fov.targetTransform = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piece"))
        {
            StartCoroutine(Collecting(other.gameObject));
        }
    }

    private IEnumerator Collecting(GameObject piece)
    {
        collectingPiece = true;
        anim.SetBool("isWalking", false);
        agent.isStopped = true;

        yield return new WaitForSeconds(2.0f);

        Destroy(piece);
        status.ReduceTotalPieces(1);
        collectingPiece = false;
        agent.isStopped = false;
        anim.SetBool("isWalking", true);
    }

    private void CollectPiece()
    {
        collectingPiece = true;
        agent.SetDestination(fov.targetTransform.position);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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