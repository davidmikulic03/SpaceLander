using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    private GravityManager gravityManager;
    Rigidbody rigidBody;
    [SerializeField] private float damageMultiplier;
    [Range(0f, 1f)]
    [SerializeField] private float fuelLeakBias;
    [SerializeField] private float fuelLeakMultiplier;

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

    public GameObject ClosestBody()
    {
        return gravityManager.closestBody;
    }

    private void FixedUpdate()
    {
        velocity = rigidBody.velocity;
        sqrSpeed = velocity.sqrMagnitude;
        speed = Mathf.Sqrt(sqrSpeed);

        FuelLeakage();
    }

    void FuelLeakage()
    {
        if(currentHealth <= 0)
        {
            currentFuel = 0;
            return;
        }

        float t = (1 - currentHealth / maxHealth);

        // EvaluateFromBias is an extended function of my creation, enter the class for a link to a desmos project that illustrates what it does.

        float evaluatedCurve = t.EvaluateFromBias(fuelLeakBias);
        float fuelLeakageRate =  maxFuel * evaluatedCurve * fuelLeakMultiplier * Time.fixedDeltaTime;
        float newFuel = currentFuel - fuelLeakageRate;

        if (newFuel >= 0f)
            currentFuel = newFuel;
        else currentFuel = 0f;
    }

    public void Damage(float speed)
    {
        if(currentHealth - speed * damageMultiplier >= 0) currentHealth -= damageMultiplier * speed;
        else currentHealth = 0;
    }
}
