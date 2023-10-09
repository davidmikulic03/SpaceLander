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

        if (speed > 10)
        {
            PlayerValues playerValues = collision.gameObject.GetComponent<PlayerValues>();

            if(playerValues != null)
            {
                playerValues.Damage(speed);
            }
        }
    }
}