using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public GameObject landerObject;
    [HideInInspector] public PlayerValues playerValues;

    public void Initialize(GameManager gameManager)
    {
        landerObject.GetComponent<LanderController>().Initialize(gameManager);
        playerValues = landerObject.GetComponent<PlayerValues>();
    }
}
