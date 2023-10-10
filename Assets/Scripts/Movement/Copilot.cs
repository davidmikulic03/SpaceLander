using UnityEngine;

public class Copilot : MonoBehaviour
{
    public PilotPreset pilotMode;
    private LanderController landerController;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        landerController = GetComponent<LanderController>();
    }

    public Vector3 pilotAssist
    {
        get
        {
            
            Vector3 angularVelocity = rb.angularVelocity;

            switch(pilotMode)
            {
                case PilotPreset.Stabilization: return Vector3.zero;
                case PilotPreset.Prograde: return Vector3.zero;
                case PilotPreset.Retrograde: return Vector3.zero;
                case PilotPreset.Up: return Vector3.zero;
            }

            return Vector3.zero;
        }
    }

    public Vector3 targetDirection 
    {  
        get 
        {
            if(rb != null)
            {
                Vector3 velocityDirection = rb.velocity.normalized;

                switch (pilotMode)
                {
                    case PilotPreset.Prograde: return velocityDirection;
                    case PilotPreset.Retrograde: return -velocityDirection;
                    case PilotPreset.Up: return Vector3.zero;
                }
            }
            return Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + targetDirection * 2);
    }
}

public enum PilotPreset
{
    Stabilization,
    Prograde,
    Retrograde,
    Up
}