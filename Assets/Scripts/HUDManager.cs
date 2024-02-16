using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance {  get; private set; }

    public Slider HealthBar;
    public Gradient HealthGradient;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
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
}
