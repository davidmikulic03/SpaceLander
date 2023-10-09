using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnim : MonoBehaviour
{
    [SerializeField] float cycleDuration;
    public float deathTime;

    [SerializeField] float scaleFactor;
    [SerializeField] float rotFactor;

    private float inverseDeathTime;
    private float inverseRot;

    private Vector3 initialScale;
    private float initialRotSpeed;

    bool isDeath;

    private void Awake()
    {
        inverseRot = 1 / cycleDuration;
        inverseDeathTime = 1 / deathTime;

        initialScale = transform.localScale;
        initialRotSpeed = inverseRot;
    }

    
    void Update()
    {
        if(!isDeath)
        {
            PassiveRotation();
        }
    }

    float t = 0;
    void PassiveRotation()
    {
        float returnTime = (1 - Mathf.Cos(2 * Mathf.PI * t)) / 2;

        inverseRot = Mathf.Lerp(initialRotSpeed, initialRotSpeed * rotFactor, 1 - returnTime);
        transform.rotation = Quaternion.AngleAxis(inverseRot, transform.forward) * transform.rotation;
        transform.localScale = Vector3.Lerp(initialScale, initialScale * scaleFactor, returnTime);

        t += Time.deltaTime / cycleDuration;
        if (t > 1)
            t--;
    }

    public IEnumerator DeathAnim()
    {
        isDeath = true;

        Vector3 startScale = transform.localScale;
        float scale = startScale.x;
        float increment = inverseDeathTime * transform.localScale.x * Time.deltaTime;

        while (true)
        {
            if(scale > 0)
            {
                scale -= increment;
                inverseRot *= 1 + 2 * Time.deltaTime * inverseDeathTime;
                transform.localScale = startScale * scale;
                transform.rotation = Quaternion.AngleAxis(inverseRot, transform.forward) * transform.rotation;
            }
            else transform.localScale = Vector3.zero;
            

            yield return null;
        }
    }
}
