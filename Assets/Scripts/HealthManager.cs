using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;
    public static event Action OnPlayerDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        HUDManager.Instance.HealthBar.maxValue = maxHealth;
        HUDManager.Instance.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) { TakeDamage(10); }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        HUDManager.Instance.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        HUDManager.Instance.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Yikes, you dead!");
        OnPlayerDeath?.Invoke();
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public bool IsFull()
    {
        return currentHealth == maxHealth;
    }

    public bool IsDead() => currentHealth == 0;

    #region Instancer
    private static HealthManager _Instance;
    public static HealthManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<HealthManager>();
            }
            return _Instance;
        }
    }
    #endregion
}
