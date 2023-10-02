using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public float fuelBurnRate;

    public float maxFuel;
    public float maxHealth;

    public float initialFuel;
    public float initialHealth;

    public void Initialize(PlayerValues playerValues)
    {
        playerValues.fuelBurnRate = fuelBurnRate;
        playerValues.maxFuel = maxFuel;
        playerValues.maxHealth = maxHealth;
        playerValues.currentFuel = initialFuel;
        playerValues.currentHealth = initialHealth;
    }
}
