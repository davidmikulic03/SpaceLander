using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject lander;
    [Header ("Scrolling")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float lerpSpeed;
    private float currentDistance = 10;
    private float targetDistance = 10;

    [Header("Rotating")]
    [SerializeField] float panSpeed;
    private Quaternion newRotation = Quaternion.identity;

    private Vector2 mouseDelta;
    private float scrollDelta;
    private bool rightMouseHeld;

    private GravityManager gravityManager;
    private LanderController landerController;
    private GameManager gameManager;

    private void Awake()
    {
        Cursor.visible = true;
        gravityManager = lander.GetComponent<GravityManager>();
        landerController = lander.GetComponent<LanderController>();

        LoadSettings();
    }

    void LoadSettings()
    {
        panSpeed = PlayerPrefs.GetFloat("Camera Sensitivity");
        scrollSpeed = PlayerPrefs.GetFloat("Scroll Sensitivity");
    }
    
    void FixedUpdate()
    {
        if (gameManager.isRunning)
        {
            ApplyTransforms();
        }
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void ApplyTransforms()
    {
        transform.rotation = RotateView();
        targetDistance = UpdatedDistance;
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, lerpSpeed * Time.deltaTime);
        transform.position = lander.transform.position + transform.localRotation * Vector3.back * currentDistance;
    }

    private float UpdatedDistance
    {
        get
        {
            float corrector = minDistance / (maxDistance - minDistance);
            float lerp = targetDistance * corrector - corrector;
            float smoothStep = 3 * lerp * lerp - 2 * lerp * lerp * lerp;

            if (Mathf.Sign(scrollDelta) == -Mathf.Sign(targetDistance - (maxDistance - minDistance) / 2))
            {
                smoothStep = lerp;
            }

            float newDistance = targetDistance;

            newDistance += scrollSpeed * scrollDelta * smoothStep * Time.deltaTime;
            newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);

            return newDistance;
        }
    }

    Vector3 oldUp = Vector3.up;
    GameObject oldClosestPlanet;
    bool isSwitching;
    private Quaternion RotateView()
    {
        GameObject closestPlanet = gravityManager.closestBody;

        // These two vectors will be used to create our new rotation
        Vector3 upVector = landerController.upVector;
        Vector3 rightVector = Vector3.Cross(upVector, -transform.forward).normalized;
        rightVector = rightVector == Vector3.zero ? transform.right : rightVector;

        if (rightMouseHeld)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float changeCoefficient = Time.fixedDeltaTime * panSpeed;

            // This quaternion rotates around the normal of the closest planet
            Quaternion localPan = Quaternion.identity;
            localPan = Quaternion.AngleAxis(mouseDelta.x * changeCoefficient, upVector);
            localPan = Quaternion.AngleAxis(mouseDelta.y * changeCoefficient, rightVector) * localPan;

            newRotation = localPan * newRotation;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Vector3 forwardVector = Vector3.Cross(rightVector, upVector);

        // Because of incomprehensible sphere math magic, we have to correct the camera z-rotation.
        float zCorrect = Vector3.SignedAngle(transform.right, -rightVector, transform.forward);
        Quaternion correctiveRotation = Quaternion.identity;

        if (isSwitching) oldUp = upVector;

        if (oldClosestPlanet == closestPlanet)
        {
            correctiveRotation = Quaternion.FromToRotation(oldUp, upVector);
            isSwitching = false;
        }
        else isSwitching = true;

        // We correct the location a little bit at a time, so that when we switch parent objects, it doesn't just snap into place.
        correctiveRotation = Quaternion.AngleAxis(zCorrect * 0.01f, transform.forward) * correctiveRotation;

        newRotation = correctiveRotation * newRotation;
        oldUp = upVector;
        oldClosestPlanet = closestPlanet;

        return newRotation;
    }

    void OnLook(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }
    void OnScroll(InputValue value)
    {
        scrollDelta = -value.Get<float>();
    }
    void OnClick(InputValue value)
    {
        if(value.Get<float>() == 1) 
            rightMouseHeld = true;
        else rightMouseHeld = false;
    }
}
