using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject prefabToSpawnOnHit;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 hitPoint = collision.contacts[0].point;
        var damage = collision.gameObject.GetComponent<IDamageable>();
        if (damage != null) damage.TakeDamage(10);
        Instantiate(prefabToSpawnOnHit, hitPoint, Quaternion.identity);
        SoundManager.PlayRandom("ExplosionHit");
        Destroy(gameObject);
    }
}
