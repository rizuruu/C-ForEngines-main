using System.Linq;
using UnityEngine;

public class AI_IntellectDevourer : AIBase
{
    private Animator anim;
    public float attackCooldown = 1f; 
    private float lastAttackTime = 0f; 

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    protected override void Update()
    {
        if (IsDead || enemyState == EnemyState.Dead) return;

        base.Update();

        if (enemyState == EnemyState.Attacking && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        if (target != null && target.GetComponent<IDamageable>() != null)
        {
            target.GetComponent<IDamageable>().TakeDamage(damageAmount);
            lastAttackTime = Time.time; 
        }
    }

    protected override void ChangeState(EnemyState newState)
    {
        if (IsDead || enemyState == EnemyState.Dead) return;

        base.ChangeState(newState);

        anim.SetBool("isChasing", newState == EnemyState.Chasing);
        anim.SetBool("isAttacking", newState == EnemyState.Attacking);
    }

    protected override void OnPlayerDetected(Transform playerTransform)
    {
    }

    protected override void OnPlayerLost()
    {
        ChangeState(EnemyState.Idle);
    }

    protected override void OnPlayerInAttackRange()
    {

    }

    public override void TakeDamage(int amount)
    {
        if (IsDead || enemyState == EnemyState.Dead) return;

        base.TakeDamage(amount);
        Debug.Log("taking damage");
        anim.SetTrigger("Hit");
        VFXManager.PlayDamageEffect(gameObject);
    }

    protected override void Die()
    {
        base.Die();
        GetComponents<Collider2D>().ToList().ForEach(collider => collider.enabled = false);
        ChangeState(EnemyState.Dead); 
        anim.SetTrigger("Dead"); 
        agent.isStopped = true;
        Destroy(gameObject, 2f);
    }
}