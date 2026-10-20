
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private EnemyAi enemyAI;

    private bool isDead;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponent<EnemyAi>();
    }

    void Update()
    {
        if (animator == null || enemyAI == null)
            return;

        // Death takes priority over all other animations.
        if (enemyAI.health <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                animator.SetBool("IsDead", true);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsWalking", false);
            }

            return;
        }

        // Walk when the player is detected; otherwise idle.
        animator.SetBool("IsWalking", enemyAI.playerInSightRange);

        // Attack when the player is within attack range.
        animator.SetBool("IsAttacking", enemyAI.playerInAttackRange);
    }
}
