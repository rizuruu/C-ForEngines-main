using System.Collections;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    public float damageInterval = 1f; 
    private Coroutine damageCoroutine; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(ApplyDamageOverTime(damageable, 15));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null; 
        }
    }

    private IEnumerator ApplyDamageOverTime(IDamageable damageable, int damageAmount)
    {
        while (true)
        {
            damageable.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
