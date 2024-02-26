using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ExplosionPrefab;
    [SerializeField] private GameObject m_PotionPrefab;

    public static VFXManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static GameObject CreateExplosion(Vector3 position, float deathTime = 0.5f)
    {
        if (!ValidateInstance()) return null;

        GameObject explosion = Instantiate(Instance.m_ExplosionPrefab, position, Quaternion.identity);

        Destroy(explosion, deathTime);

        return explosion;
    }

    public static GameObject SpawnEffect(GameObject fxObject, Vector3 position, float deathTime = 0.5f)
    {
        if (!ValidateInstance()) return null;

        GameObject fx = Instantiate(fxObject, position, Quaternion.identity);

        Destroy(fx, deathTime);

        return fx;
    }

    public static GameObject SpawnPotionEffect(Transform parent, float deathTime = 0.5f)
    {
        if (!ValidateInstance()) return null;

        GameObject fx = Instantiate(Instance.m_PotionPrefab, parent);

        Destroy(fx, deathTime);

        return fx;
    }

    public static void PlayDamageEffect(GameObject gameObject, float flashDuration = 0.5f, int flashCount = 3)
    {
        if (!ValidateInstance()) return;

        Instance.FlashRedOnDamage(gameObject, flashDuration, flashCount);
    }

    public void FlashRedOnDamage(GameObject gameObject, float flashDuration, int flashCount)
    {
        StopCoroutine(DamageFlashCoroutine(gameObject, flashDuration, flashCount));
        StartCoroutine(DamageFlashCoroutine(gameObject, flashDuration, flashCount));
    }

    private IEnumerator DamageFlashCoroutine(GameObject gameObject, float flashDuration, int flashCount)
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float singleFlashDuration = flashDuration / (flashCount * 2);

        for (int i = 0; i < flashCount * 2; i++)
        {
            // Toggle color between original and red
            spriteRenderer.color = spriteRenderer.color == Color.white ? Color.red : Color.white;

            // Wait for half the duration of a single flash before toggling color
            yield return new WaitForSeconds(singleFlashDuration);
        }
    }

    static bool ValidateInstance()
    {
        if (Instance == null)
        {
            Debug.LogError("Tried to spawn an effect, but instance hasn't been set.");
            return false;
        }
        return true;
    }
}
