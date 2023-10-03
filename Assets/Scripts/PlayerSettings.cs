using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [Header("Camera Settings")]
    [Range(0f, 10f)]
    public float cameraSensitivity = 5f;
    [Range(0f, 5f)]
    public float scrollSpeed = 2.5f;
}
