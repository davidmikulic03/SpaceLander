using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    private GravityManager gravityManager;
    Rigidbody rigidBody;
    [SerializeField] private float damageMultiplier;

    [HideInInspector] public float fuelBurnRate = 0.1f;
    [HideInInspector] public float maxFuel = 100f;
    [HideInInspector] public float maxHealth = 100f;

    public float currentFuel;
    public float currentHealth;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public float sqrSpeed;
    public float speed;

    public int score;

    public void Initialize(SceneSettings sceneSettings) 
    {
        this.fuelBurnRate = sceneSettings.fuelBurnRate;
        this.maxFuel = sceneSettings.maxFuel;
        this.maxHealth = sceneSettings.maxHealth;
        currentFuel = sceneSettings.initialFuel;
        currentHealth = sceneSettings.initialHealth;

        rigidBody = GetComponent<Rigidbody>();
        gravityManager = GetComponent<GravityManager>();
    }

    public PlayerValues(Rigidbody rigidBody, float fuelBurnRate, float maxFuel, float maxHealth, float initialFuel, float initialHealth)
    {
        this.rigidBody = rigidBody;
        this.fuelBurnRate = fuelBurnRate;
        this.maxFuel = maxFuel;
        this.maxHealth = maxHealth;
        this.currentFuel = initialFuel;
        this.currentHealth = initialHealth;
    }

    public GameObject ClosestBody()
    {
        return gravityManager.closestBody;
    }

    private void FixedUpdate()
    {
        velocity = rigidBody.velocity;
        sqrSpeed = velocity.sqrMagnitude;
        speed = Mathf.Sqrt(sqrSpeed);
    }

    private void Update()
    {
        currentFuel -= (1 - currentHealth / maxHealth) * Time.deltaTime;
    }

    public void Damage(float damage)
    {
        if(currentFuel > 0) currentHealth -= damageMultiplier * damage;
        else currentHealth = 0;
    }
}
