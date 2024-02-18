using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.5f;

    private Vector3 originalPosition;
    private float shakeTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            originalPosition = transform.localPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        // If shakeTimer is running
        if (shakeTimer > 0)
        {
            transform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                // When shake is over, reset position
                transform.localPosition = originalPosition;
            }
        }
    }

    public static void ShakeCamera(float duration, float magnitude)
    {
        if (Instance != null)
        {
            Instance.shakeDuration = duration;
            Instance.shakeMagnitude = magnitude;
            Instance.shakeTimer = duration; 
        }
        else
        {
            Debug.LogWarning("Camera Shake instance not found");
        }
    }

    public static void DoShake()
    {
        ShakeCamera(Instance.shakeDuration, Instance.shakeMagnitude);
    }
}