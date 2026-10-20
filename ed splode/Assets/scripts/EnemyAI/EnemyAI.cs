
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject enemyWeapon;
    public float health = 50f;
    public Renderer enemyRenderer;
    public Color normalColour;
    public Color hitColour = Color.red;
    public GameObject deathEffect;
    public Transform projectileSpawn;
    public Transform deathEffectSpawn;

    //looking around
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    IEnumerator FlashRed()
    {
        Material material = enemyRenderer.materials[1];

        Color originalColour = material.color;

        material.color = hitColour;

        yield return new WaitForSeconds(0.2f);

        material.color = originalColour;
    }

    private void Awake()
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (health <= 0)
        {   
            agent.isStopped = true;
             return;
        }
        
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        {
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        Vector3 lookPosition = player.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);

        if (!alreadyAttacked)
        {
            ///ATTACK CODE!!
            Rigidbody rb = Instantiate(
                enemyWeapon,
                projectileSpawn.position,
                Quaternion.identity
            ).GetComponent<Rigidbody>();

            Collider bulletCollider = rb.GetComponent<Collider>();

            if (bulletCollider != null)
            {
                Collider[] enemyColliders = GetComponentsInChildren<Collider>();

                foreach (Collider enemyCollider in enemyColliders)
                {
                    Physics.IgnoreCollision(bulletCollider, enemyCollider);
                }
            }

            rb.AddForce(
                (player.position - projectileSpawn.position).normalized * 32f,
                ForceMode.Impulse
            );

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        ///Take damage code here
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Instantiate(deathEffect, deathEffectSpawn.position, Quaternion.identity);
            Invoke(nameof(DestroyEnemy), 3f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
