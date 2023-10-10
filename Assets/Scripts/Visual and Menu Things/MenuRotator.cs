using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotator : MonoBehaviour
{
    Vector3 initialPosition;
    float initialRotation;

    [SerializeField] private float passiveRotation;

    private Vector3 axis;

    [SerializeField] float moveTime;
    [SerializeField] Vector3 secondPosition;
    [SerializeField] float moveRotationMultiplier = 2;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = passiveRotation;
        RandomizeAxis();
    }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(passiveRotation * Time.deltaTime, axis) * transform.rotation;
    }

    void RandomizeAxis()
    {
        axis = new Vector3(2 * Random.value - 1, 2 * Random.value - 1, 2 * Random.value - 1).normalized;
    }

    public void ToLevel()
    {
        RandomizeAxis();
        StopAllCoroutines();
        StartCoroutine(MoveLander(moveTime, secondPosition));
    }

    public void ToMenu()
    {
        RandomizeAxis();
        StopAllCoroutines();
        StartCoroutine(MoveLander(moveTime, initialPosition));
    }

    IEnumerator MoveLander(float duration, Vector3 destination)
    {
        float t = 0;
        Vector3 position = transform.position;

        while (t < moveTime)
        {
            float normalizedTime = t / duration;
            float smoothTime = 3 * normalizedTime * normalizedTime - 2 * normalizedTime * normalizedTime * normalizedTime;
            float smoothTurn = 4 * normalizedTime - 4 * normalizedTime * normalizedTime;

            transform.position = Vector3.Lerp(position, destination, smoothTime);
            passiveRotation = Mathf.Lerp(initialRotation, initialRotation * moveRotationMultiplier, smoothTurn);

            t += Time.deltaTime;
            yield return null;
        }

        passiveRotation = initialRotation;
    }
}
