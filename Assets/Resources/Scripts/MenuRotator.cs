using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotator : MonoBehaviour
{
    Transform initialTransform;
    [SerializeField] private float passiveRotation;

    private Vector3 axis;

    private void Start()
    {
        initialTransform = transform;
        RandomizeAxis();
    }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(passiveRotation * Time.deltaTime, axis) * transform.rotation;
    }

    void RandomizeAxis()
    {
        axis = new Vector3(Random.value, Random.value, Random.value).normalized;
    }

    private void Reset()
    {
        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
    }
}
