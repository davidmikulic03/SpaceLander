using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (!rb) return;

        GameObject playerObject = collision.gameObject;

        Vector3 normalVector = -(playerObject.transform.position - transform.position).normalized;
        Vector3 velocity = rb.velocity + rb.angularVelocity * playerObject.transform.localScale.x;
        float dottedSpeed = Mathf.Abs(Vector3.Dot(normalVector, velocity));
        //float influence = 0.9f;

        if (dottedSpeed > 2)
        {
            PlayerValues playerValues = playerObject.GetComponent<PlayerValues>();

            if(playerValues != null)
            {
                playerValues.Damage(dottedSpeed - 2);
            }
        }
    }
}