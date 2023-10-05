using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalBody : MonoBehaviour
{
    [SerializeField] GravitationalConstant gravitationalConstant;
    [SerializeField] private float surfaceGravity;
    [HideInInspector] public float mass;

    private void Awake()
    {
        float radius = transform.localScale.x / 2;
        float squareRadius = radius * radius;
        mass = surfaceGravity * squareRadius / gravitationalConstant.value;

        
    }
}
