using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public abstract void OnPickup(TopDownCharacterController player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.GetComponent<TopDownCharacterController>());
        }
    }

    public void Destroy() => Destroy(gameObject);
}
