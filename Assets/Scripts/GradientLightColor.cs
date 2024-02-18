using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GradientLightColor : MonoBehaviour
{
    public Gradient colorGradient; 
    public float cycleDuration = 2f; 

    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        float cycleFraction = Mathf.PingPong(Time.time / cycleDuration, 1f);

        light2D.color = colorGradient.Evaluate(cycleFraction);
    }
}