using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    Rigidbody rigidBody;

    public float fuelBurnRate = 0.1f;

    public float maxFuel = 100f;
    public float maxHealth = 100f;

    public Vector3 velocity;
    public float speed;
    public float currentFuel;
    public float currentHealth;

    public void Initialize(SceneData sceneData) 
    {
        this.fuelBurnRate = sceneData.fuelBurnRate;
        this.maxFuel = sceneData.maxFuel;
        this.maxHealth = sceneData.maxHealth;
        currentFuel = sceneData.initialFuel;
        currentHealth = sceneData.initialHealth;
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
}
