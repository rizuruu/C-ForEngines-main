using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance {  get; private set; }

    public Slider HealthBar;
    public Gradient HealthGradient;

    [Header("Pause Menu")]
    public GameObject PauseMenuPanel;

    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void SetHealth(int health)
    {
        if (HealthBar != null)
        {
            HealthBar.value = health;

            float healthPercent = 1f - (float)health / HealthBar.maxValue;
            HealthBar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(healthPercent);
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        GameManager.Instance.TogglePause();
        Time.timeScale = isPaused ? 0.0f : 1.0f;

        PauseMenuPanel.SetActive(isPaused);
    }
    
    public void Resume()
    {
        isPaused = false;
        GameManager.Instance.TogglePause(isPaused);
        Time.timeScale = 1.0f;
        PauseMenuPanel.SetActive(false);
    }
}
