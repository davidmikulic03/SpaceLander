using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanderController : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody rigidBody;
    [SerializeField] private GameObject cameraObject;
    [Header("Reaction Control System (Rotation)")]

    [SerializeField] private float puffStrength;
    [SerializeField] private float thrustStrength;

    private Vector3 rotationInput;
    private float thrustInput;

    [HideInInspector] public Vector3 upVector;

    [SerializeField] private bool stabilization;

    private GravityManager gravityManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        gravityManager = GetComponent<GravityManager>();
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
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
        rigidBody.velocity +=
                transform.up *
                thrustStrength *
                thrustInput *
                Time.deltaTime;
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

                //Quaternion alignForward = Quaternion.FromToRotation(cameraObject.transform.up, transform.up);
                //Quaternion alignRight = Quaternion.FromToRotation(cameraPlaneNormal, transform.forward);

                Vector3 forwardAxle = Vector3.ProjectOnPlane(-cameraObject.transform.forward, transform.up).normalized;
                forwardAxle.Normalize();
                globalImpulse += forwardAxle * rotationInput.x;

                Vector3 rightAxle = cameraObject.transform.right;
                globalImpulse += rightAxle * rotationInput.y;

                Vector3 upAxle = transform.up;
                globalImpulse += upAxle * rotationInput.z;
                
                globalImpulse *= puffStrength * 
                    Time.deltaTime;

                return globalImpulse;
            }
            else if (stabilization)
            {
                Vector3 rbAngularVelocity = rigidBody.angularVelocity;
                Vector3 output = -rbAngularVelocity.normalized * puffStrength * Time.deltaTime;
                if (Mathf.Abs(output.magnitude) > Mathf.Abs(rbAngularVelocity.magnitude))
                    output = -rbAngularVelocity;
                return output;
            }
            return Vector3.zero;
        }
    }

    
    public void GetUpVector()
    {
        GameObject closestPlanet = gravityManager.ClosestPlanet();
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
