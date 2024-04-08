using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FBIControl : MonoBehaviour
{
    /*
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;

        //Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;


        private void Awake()
        {
            player = GameObject.Find("Alien").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) Chase();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }

        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;

        }

        private void SearchWalkPoint()
        {
            // Calculate random ponit in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            // Check if the walkPoint is on the ground.
            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        private void Chase()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {

        }
        */

    private PieceSpawn pieceSpawn;
    private List<GameObject> collectedPieces = new List<GameObject>();
    private StatusManagement status;

    public float speed = 3f;

    void Start()
    {
        pieceSpawn = GameObject.FindObjectOfType<PieceSpawn>();
        status = GameObject.FindObjectOfType<StatusManagement>();
        StartCoroutine(CollectPieces());
    }

    IEnumerator CollectPieces()
    {
        yield return new WaitForSeconds(10f);

        while (true)
        {
            var piecePositions = pieceSpawn.GetPiecePositions();

            // Filter out the positions of pieces that haven't been collected yet
            List<Vector3> uncollectedPiecePositions = new List<Vector3>();
            foreach (Vector3 piecePosition in piecePositions)
            {
                if (!collectedPieces.Contains(pieceSpawn.GetPieceAtPosition(piecePosition)))
                {
                    uncollectedPiecePositions.Add(piecePosition);
                }
            }

            if (uncollectedPiecePositions.Count > 0)
            {

                // Find the closest piece position among the uncollected pieces
                float minDistance = float.MaxValue;
                Vector3 targetPosition = Vector3.zero;
                foreach (Vector3 piecePosition in uncollectedPiecePositions)
                {
                    float distance = Vector3.Distance(transform.position, piecePosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetPosition = piecePosition;
                    }
                }

                // Move towards the closest uncollected piece position
                
                while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    Vector3 direction = targetPosition - transform.position;
                    transform.Translate(direction.normalized * speed * Time.deltaTime);
                    yield return null;
                }

                GameObject piece = pieceSpawn.GetPieceAtPosition(targetPosition);
                // Add the collected piece to the list
                collectedPieces.Add(piece);
                // Destroy the piece
                Destroy(piece);
                // Perform any actions related to collecting the piece (e.g., update score)
                status.ReduceTotalPieces(1);

                yield return new WaitForSeconds(10f);

            }
            else
            {
                yield break;
            }
        }
    }
}
