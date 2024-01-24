using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;
    [SerializeField] float m_moveSpeed = 2.0f; // Use float for more precise movement
    private float t = 0.5f; // Interpolation parameter

    private void Update()
    {
        // Increment the interpolation parameter based on the moveSpeed and Time.deltaTime
        t += m_moveSpeed * Time.deltaTime;

        // Use Mathf.PingPong to smoothly interpolate between the start and end points
        float pingPongT = Mathf.PingPong(t, 1.0f);

        // Use Lerp to interpolate between the start and end points using the pingPongT value
        transform.position = Vector3.Lerp(m_startPoint.position, m_endPoint.position, pingPongT);
    }
}
