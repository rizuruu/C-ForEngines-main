using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;
    [SerializeField] float m_moveSpeed = 2.0f;
    private float t = 0.5f; 

    private void Update()
    {
        t += m_moveSpeed * Time.deltaTime;

        float pingPongT = Mathf.PingPong(t, 1.0f);

        transform.position = Vector3.Lerp(m_startPoint.position, m_endPoint.position, pingPongT);
    }
}
