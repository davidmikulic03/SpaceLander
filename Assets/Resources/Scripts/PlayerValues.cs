using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    Rigidbody rigidBody;

    public float maxFuel = 100f;
    public float maxHealth = 100f;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public float speed;
    [HideInInspector] public float currentFuel;
    [HideInInspector] public float currentHealth;

    public void Initialize(float maxFuel, float maxHealth, float currentFuel, float currentHealth)
    {
        this.maxFuel = maxFuel; 
        this.maxHealth = maxHealth; 
        this.currentFuel = currentFuel; 
        this.currentHealth = currentHealth;
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        velocity = rigidBody.velocity;
        speed = velocity.magnitude;
    }
}
