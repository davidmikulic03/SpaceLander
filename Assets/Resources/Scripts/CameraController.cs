using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        Cursor.visible = true;
        gravityManager = lander.GetComponent<GravityManager>();
        landerController = lander.GetComponent<LanderController>();
    }

    
    void Update()
    {
        ApplyTransforms();
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

    Vector3 upVector;
    Vector3 rightVector;

    Vector3 oldUp = Vector3.up;
    GameObject oldClosestPlanet;
    bool isSwitching;
    private Quaternion RotateView()
    {
        GameObject closestPlanet = gravityManager.ClosestPlanet();

        upVector = landerController.upVector;
        rightVector = Vector3.Cross(upVector, -transform.forward).normalized;
        rightVector = rightVector == Vector3.zero ? transform.right : rightVector;

        if (rightMouseHeld)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float changeCoefficient = Time.deltaTime * panSpeed;

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

        float zCorrect = Vector3.SignedAngle(transform.right, -rightVector, forwardVector);

        Quaternion correctiveRotation = Quaternion.identity;

        if (isSwitching) oldUp = upVector;

        if (oldClosestPlanet == closestPlanet)
        {
            correctiveRotation = Quaternion.FromToRotation(oldUp, upVector);
            isSwitching = false;
        }
        else isSwitching = true;

        correctiveRotation = Quaternion.AngleAxis(zCorrect * 0.01f, forwardVector) * correctiveRotation;

        newRotation = correctiveRotation * newRotation;
        oldUp = upVector;
        oldClosestPlanet = closestPlanet;

        return newRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(transform.position, upVector));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, oldUp));
    }

    void OnLook(InputValue value)
    {
        mouseDelta = value.Get<Vector2>() / Time.timeScale;
    }
    void OnScroll(InputValue value)
    {
        scrollDelta = -value.Get<float>() / Time.timeScale;
    }
    void OnClick(InputValue value)
    {
        if(value.Get<float>() == 1) 
            rightMouseHeld = true;
        else rightMouseHeld = false;
    }
}
