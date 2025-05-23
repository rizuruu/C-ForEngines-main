using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_speed;
    public float m_stoppingDistance;

    private Transform m_Player;
    private bool m_PlayerInSight = false;
    private EnemyState m_enemyStates = EnemyState.Idle;

    private void Start()
    {
        m_Player = FindObjectOfType<TopDownCharacterController>().transform;
    }

    private void Update()
    {
        switch (m_enemyStates)
        {
            case EnemyState.Idle:
                // Idle State
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
            case EnemyState.Attacking:
                // Attack state
                break;
        }
    }

    private void ChasePlayer()
    {
        if (m_PlayerInSight && Vector2.Distance(transform.position, m_Player.position) >= m_stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Player.position, m_speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_PlayerInSight = true;
            m_enemyStates = EnemyState.Chasing; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_PlayerInSight = false;
            m_enemyStates = EnemyState.Idle; 
        }
    }
}
