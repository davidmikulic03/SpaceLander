using System.Collections;
using System.Collections.Generic;
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

                Vector3 forwardAxle = Vector3.ProjectOnPlane(-cameraObject.transform.forward, transform.up).normalized;
                Vector3 rightAxle = Vector3.ProjectOnPlane(cameraObject.transform.right, transform.up).normalized;
                Vector3 upAxle = transform.up;

                globalImpulse += forwardAxle * rotationInput.x;
                globalImpulse += rightAxle * rotationInput.y;
                globalImpulse += upAxle * rotationInput.z;
                
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

    private void OnRotation(InputValue value)
    {
        rotationInput = value.Get<Vector3>();
    }

    private void OnThrust(InputValue value)
    {
        thrustInput = value.Get<float>();
    }
}
