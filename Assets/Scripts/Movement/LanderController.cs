using UnityEngine;
using UnityEngine.InputSystem;

public class LanderController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rigidBody;
    [SerializeField] private GameObject cameraObject;
    private float rotationStrength;
    private float thrustStrength;

    [HideInInspector] public bool isThrusting;
    public Vector3 rotationInput;
    public float thrustInput;

    [HideInInspector] public Vector3 upVector;

    [SerializeField] private bool stabilization;

    private GravityManager gravityManager;
    private PlayerValues playerValues;

    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        gravityManager = GetComponent<GravityManager>();
        playerValues = GetComponent<PlayerValues>();
    }

    public void Initialize(GameManager gameManager, SceneSettings sceneSettings)
    {
        this.gameManager = gameManager;

        rotationStrength = sceneSettings.rotationStrength;
        thrustStrength = sceneSettings.thrustStrength;
        stabilization = sceneSettings.stabilization;
    }

    void Update()
    {
        Movement();
    }

    void ApplyRCS()
    {
        rigidBody.angularVelocity += AngularThrust;
        
    }

    void ApplyThrust()
    {
        float engineEfficiency = playerValues.fuelBurnRate;
        float currentFuel = playerValues.currentFuel;

        if(currentFuel > 0)
        {
            playerValues.currentFuel -= thrustInput * engineEfficiency * Time.deltaTime;

            rigidBody.velocity +=
                transform.up *
                thrustStrength *
                thrustInput *
                Time.deltaTime;
            
            if(thrustInput > 0)
            {
                isThrusting = true;
                return;
            }
        }
        else playerValues.currentFuel = 0;
        
        isThrusting = false;
    }

    void Movement()
    {
        GetUpVector();
        ApplyRCS();
        ApplyThrust();
    }

    Vector3 AngularThrust
    {
        get
        {
            if(rotationInput != Vector3.zero)
            {
                Vector3 globalImpulse = Vector3.zero;

                // These three lines use the plane defined by the landerModel's up-vector to create a space based on the local space,
                // but essentially rotated along the local y to align with the camera. The upAxle is unchanged, because it is not
                // changed by being rotated along its own axis.

                // It looks a bit like magic, but if you consider what ProjectOnPlane does and use your hands to visualise, it is
                // somewhat understandable.
                Vector3 forwardAxle = Vector3.ProjectOnPlane(-cameraObject.transform.forward, transform.up).normalized;
                Vector3 rightAxle = Vector3.ProjectOnPlane(cameraObject.transform.right, transform.up).normalized;
                Vector3 upAxle = transform.up;

                // These lines transform the world space input to this new defined space
                globalImpulse += forwardAxle * rotationInput.x;
                globalImpulse += rightAxle * rotationInput.y;
                globalImpulse += upAxle * rotationInput.z;
                
                // The transformed input is transformed into a velocity to be added.
                globalImpulse *= rotationStrength * 
                    Time.deltaTime;

                return globalImpulse;
            }
            else if (stabilization)
            {
                Vector3 rbAngularVelocity = rigidBody.angularVelocity;
                Vector3 output = -rbAngularVelocity.normalized * rotationStrength * Time.deltaTime;
                if (Mathf.Abs(output.magnitude) > Mathf.Abs(rbAngularVelocity.magnitude))
                    output = -rbAngularVelocity;
                return output;
            }
            return Vector3.zero;
        }
    }

    public void Refuel(float amount)
    {
        playerValues.currentFuel += amount;
        playerValues.currentFuel = Mathf.Clamp(playerValues.currentFuel, 0, playerValues.maxFuel);
    }

    public void Repair(float amount)
    {
        playerValues.currentHealth += amount;
        playerValues.currentHealth = Mathf.Clamp(playerValues.currentHealth, 0, playerValues.maxHealth);
    }

    public void GetUpVector()
    {
        GameObject closestPlanet = gravityManager.closestBody;
        Vector3 closestPlanetDistance = closestPlanet.transform.position;
        upVector = -(closestPlanetDistance - transform.position).normalized;
    }
    

    Transform cameraTransform
    {
        get { return cameraObject.transform; }
    }

    // The Player Input component calls these methods to assign values to our input variables.
    private void OnRotation(InputValue value)
    {
        rotationInput = value.Get<Vector3>();
    }

    private void OnThrust(InputValue value)
    {
        thrustInput = value.Get<float>();
    }
}
