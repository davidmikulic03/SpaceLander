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

    public int points;

    public void Initialize(SceneSettings sceneSettings) 
    {
        this.fuelBurnRate = sceneSettings.fuelBurnRate;
        this.maxFuel = sceneSettings.maxFuel;
        this.maxHealth = sceneSettings.maxHealth;
        currentFuel = sceneSettings.initialFuel;
        currentHealth = sceneSettings.initialHealth;
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
