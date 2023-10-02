using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    [SerializeField] TextMeshProUGUI fuelText;

    
    
    void Update()
    {
        fuelText.text = playerValues.currentFuel.ToString();
    }
}
