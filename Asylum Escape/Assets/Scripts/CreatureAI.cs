using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public Vector3 lastwalkPoint;
    [SerializeField] public bool walkPointSet;
    public float walkPointRange;
    private Vector3 lastPosition;
    public float idleTimer = 0;
    public float idleTimeThreshold = 5;

    //States
    [SerializeField] public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;
    public bool isWalking, isAttacking;

    //Attack parameters
    [SerializeField] public float attackDelay;


    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        isWalking = true;
        isAttacking = false;
        lastPosition = transform.position;
    }


    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //if (playerInSightRange && Math.Abs(transform.position.y - player.position.y) > 4) playerInSightRange = false;
        //if (playerInSightRange)
        //{
        //    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //    Vector3 directionToPlayer = (player.position - transform.position).normalized;
        //    if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, sightRange, whatIsGround | whatIsPlayer))
        //    {
        //        if (hit.collider.name.StartsWith("Door") || hit.collider.name.StartsWith("Hall") || hit.collider.name.StartsWith("Wood"))
        //        {
        //            playerInSightRange = false;
        //        }
        //    }
        //}
        //Debug.Log(agent.pathStatus);
        //if (playerInSightRange && agent.pathStatus == NavMeshPathStatus.PathPartial)
        //{
        //    playerInSightRange = false;
        //}

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (Vector3.Distance(transform.position, lastPosition) == 0)
        {
            idleTimer += Time.deltaTime;
        }
        else
        {
            idleTimer = 0; 
        }

        lastPosition = transform.position;

    }

    private void AttackPlayer()
    {
        if (isAttacking == true)
        {
            return;
        }

        isAttacking = true;
        isWalking = false;
        walkPointSet = false;

        // Define Attack behaviour once we have Health System etc.
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        float rotationSpeed = 10f; 
        bool isRotating = true;

        while (isRotating)
        {
            Vector3 facingDirection = (player.position - transform.position).normalized;

            // Ignore the Y-axis rotation by zeroing out the Y component
            facingDirection.y = 0;

            // Create the target rotation towards the player
            Quaternion targetRotation = Quaternion.LookRotation(facingDirection);

            // Smoothly rotate the creature towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Check if the rotation is complete (or close enough to be considered done)
            //print(targetRotation);
            //print(transform.rotation);
            //print(Quaternion.Angle(transform.rotation, targetRotation));

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isRotating = false;
            }

            yield return null; // Wait for the next frame
        }


        // Wait for the attack delay before setting isAttacking to false
        yield return new WaitForSeconds(attackDelay);

        // After the delay, set isAttacking to false
        isAttacking = false;
    }

    //cred ca trebuie schimbate niste variabile si facute enum
    private void Patroling()
    {
        if (isAttacking == true)
        {
            return;
        }

        isWalking = true;
        isAttacking = false;

        if (idleTimer >= idleTimeThreshold || !walkPointSet || walkPoint == transform.position)
        {
            idleTimer = 0f;
            SearchWalkPoint();
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        //Debug.Log(agent.pathStatus);
        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            walkPointSet = false;
            SearchWalkPoint();
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (isAttacking == true)
        {
            return;
        }

        isWalking = true;
        isAttacking = false;
        walkPointSet = false;

        agent.SetDestination(player.position);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

