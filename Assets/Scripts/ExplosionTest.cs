using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTest : MonoBehaviour
{
    [SerializeField, Range(0.01f, 1f)] private float explosionFrequency = 0.1f; 
    [SerializeField, Range(0.5f, 4f)] private float explosionRadius = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionSpawnerCoroutine());
    }

    IEnumerator ExplosionSpawnerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(explosionFrequency);

            Vector3 explosionPos = transform.position;
            explosionPos += (Vector3)Random.insideUnitCircle.normalized * explosionRadius;

            VFXManager.CreateExplosion(explosionPos);
        }
    }
}
