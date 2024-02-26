using UnityEngine;
using UnityEngine.AI;

public abstract class AIBase : MonoBehaviour, IDamageable
{
    public bool canPatrol = true;
    public EnemyState enemyState = EnemyState.Idle;
    public int damageAmount = 10;

    // Health properties
    public int maxHealth = 100;

    protected int currentHealth;
    protected NavMeshAgent agent;
    protected Transform target;

    private bool playerInRange = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        if (HealthManager.Instance.IsDead())
        {
            if (enemyState != EnemyState.Idle)
            {
                ChangeState(EnemyState.Idle);
            }
            return;
        }

        if (target != null && playerInRange)
        {
            float distanceToTarget = Vector3.Distance(target.position, transform.position);
            bool withinAttackRange = distanceToTarget <= agent.stoppingDistance;

            if (withinAttackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Attacking);
            }
            else if (!withinAttackRange && enemyState != EnemyState.Chasing)
            {
                ChangeState(EnemyState.Chasing);
            }

            if (enemyState == EnemyState.Chasing)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            target = collision.transform;
            OnPlayerDetected(target);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            target = null;
            OnPlayerLost();
        }
    }

    protected virtual void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;
        enemyState = newState;

        // Handling animation state change in the base class if common across all AI,
        // or in derived classes for specific animations.
    }

    protected virtual void Attack() { }

    protected abstract void OnPlayerDetected(Transform playerTransform);
    protected abstract void OnPlayerLost();
    protected abstract void OnPlayerInAttackRange();

    public virtual void TakeDamage(int amount) 
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die() { }

    protected bool IsDead => currentHealth == 0;
}
