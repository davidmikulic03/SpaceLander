using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (!rb) return;

        float speed = rb.velocity.magnitude;

        if (speed > 5)
        {
            Debug.Log("Ouch!");
            //StartCoroutine(HitFrames());
        }
    }

    IEnumerator HitFrames()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1f;
    }
}