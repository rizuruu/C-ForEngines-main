using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ExplosionPrefab;

    public static VFXManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static GameObject CreateExplosion(Vector3 position, float deathTime = 0.5f)
    {
        if (Instance == null)
        {
            Debug.Log("Tried to spawn an explosion, but instance hasn't been set.");
            return null;
        }

        GameObject explosion = Instantiate(Instance.m_ExplosionPrefab, position, Quaternion.identity);

        Destroy(explosion, deathTime);

        return explosion;
    }
}
