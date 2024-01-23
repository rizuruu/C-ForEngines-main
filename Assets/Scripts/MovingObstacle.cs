using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;
    [SerializeField] int m_moveSpeed;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = m_startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, m_moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("MovingObstacleWaypoint"))
            ChangeTarget();
    }

    void ChangeTarget()
    {
        Debug.Log("change");
        target = m_endPoint;
    }
}
