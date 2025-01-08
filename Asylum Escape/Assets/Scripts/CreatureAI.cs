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
    bool walkPointSet;
    public float walkPointRange;

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
    }


    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (isAttacking == true)
        {
            return;
        }

        isAttacking = true;
        isWalking = false;

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


    private void Patroling()
    {
        if (isAttacking == true)
        {
            return;
        }

        isWalking = true;
        isAttacking = false;

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

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

