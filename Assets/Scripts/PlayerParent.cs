using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    [SerializeField] private GameObject landerObject;
    [SerializeField] private GameObject cameraObject;
    [HideInInspector] public PlayerValues playerValues;

    public void Initialize(GameManager gameManager, SceneSettings sceneSettings)
    {
        landerObject.GetComponent<LanderController>().Initialize(gameManager, sceneSettings);
        cameraObject.GetComponent<CameraController>().Initialize(gameManager);
        playerValues = landerObject.GetComponent<PlayerValues>();
    }
}
