using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour, IDamageable
{
    #region Framework Stuff
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    private Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    [Header("Movement parameters")]
    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;

    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_firePoint;
    [SerializeField] float m_projectileSpeed;

    private Coroutine speedBoostCoroutine;
    private float originalMaxSpeed;

    [Header("Damage Properties")]
    [SerializeField] float flashDuration = 0.5f;
    [SerializeField] float flashCount = 3;
    #endregion


    /// <summary>
    /// When the script first initialises this gets called, use this for grabbing componenets
    /// </summary>
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called after Awake(), and is used to initialize variables e.g. set values on the player
    /// </summary>
    private void Start()
    {
        
    }

    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc perfornamce so physics can be calculated properly
    /// </summary>
    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = playerDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
    }

    /// <summary>
    /// When the update loop is called, it runs every frame, ca run more or less frequently depending on performance. Used to catch changes in variables or input.
    /// </summary>
    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
            return;

        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (playerDirection.magnitude != 0)
        {
            animator.SetFloat("Horizontal", playerDirection.x);
            animator.SetFloat("Vertical", playerDirection.y);
            animator.SetFloat("Speed", playerDirection.magnitude);

            //And set the speed to 1, so they move!
            playerSpeed = 1f;

            if (Input.GetKeyDown(KeyCode.R))
                animator.SetTrigger("Rolling");
        }
        else
        {
            //Was the input just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }

        // Was the fire button pressed (mapped to Left mouse button or gamepad trigger)
        if (Input.GetButtonDown("Fire1"))
        {
            //Shoot
            Fire();
        }
    }

    void Fire()
    {
        GameObject bulletToSpawn = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
        var bulletRB = bulletToSpawn.GetComponent<Rigidbody2D>();
        if (bulletRB != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mousePointOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calculate the angle for the direction/rotation
            Vector2 direction = (mousePointOnScreen - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletToSpawn.transform.rotation = Quaternion.Euler(0, 0, angle);

            bulletRB.AddForce(direction.normalized * m_projectileSpeed, ForceMode2D.Impulse);
        }
    }

    public void BoostSpeed(float boostMultiplier, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine); 
        }
        speedBoostCoroutine = StartCoroutine(BoostSpeedRoutine(boostMultiplier, duration));
    }

    private IEnumerator BoostSpeedRoutine(float boostMultiplier, float duration)
    {
        GameObject fx = VFXManager.SpawnPotionEffect(gameObject.transform, duration);

        originalMaxSpeed = playerMaxSpeed; 
        playerMaxSpeed *= boostMultiplier; 

        yield return new WaitForSeconds(duration); 

        playerMaxSpeed = originalMaxSpeed; 
        speedBoostCoroutine = null; 
        Destroy(fx);
    }

    public void TakeDamage(int amount)
    {
        HealthManager.Instance.TakeDamage(amount);
        CameraShake.DoShake();
        VFXManager.PlayDamageEffect(gameObject);
        SoundManager.PlayRandom("TakeDamage");
    }
}
