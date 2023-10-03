using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light lightComponent;
    [SerializeField] float blinkGap;
    [SerializeField] float blinkDuration;
    [SerializeField] bool isBlinking = true;

    // Update is called once per frame
    void Start()
    {
        lightComponent = GetComponent<Light>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (isBlinking)
        {
            if (lightComponent.enabled)
            {
                lightComponent.enabled = false;
                yield return new WaitForSeconds(blinkGap);
            }
            else
            {
                lightComponent.enabled = true;
                yield return new WaitForSeconds(blinkDuration);
            }
            
        }
    }
}
