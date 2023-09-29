using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public GameObject landerObject;

    public void Initialize(GameManager gameManager)
    {
        landerObject.GetComponent<LanderController>().Initialize(gameManager);
    }
}
