using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copilot : MonoBehaviour
{
    public Pilotpreset pilotMode;

    public Vector3 pilotAssist
    {
        get
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            switch(pilotMode)
            {
                case Pilotpreset.Stabilization: return Vector3.zero;
                case Pilotpreset.Prograde: return Vector3.zero;
                case Pilotpreset.Retrograde: return Vector3.zero;
                case Pilotpreset.Gravity: return Vector3.zero;
            }

            return Vector3.zero;
        }
    }
}

public enum Pilotpreset
{
    Stabilization,
    Prograde,
    Retrograde,
    Gravity
}