using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [Header("Player Values")]
    public float fuelBurnRate;
    public float maxFuel;
    public float maxHealth;
    public float initialFuel;
    public float initialHealth;

    [Header("Movement")]
    public float thrustStrength;
    public float rotationStrength;
    public bool stabilization;
}
