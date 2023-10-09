using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] TextMeshProUGUI orbitDataText;
    [SerializeField] TextMeshProUGUI pointsText;

    void Update()
    {
        fuelText.text = playerValues.currentFuel.ToString() + " / " + playerValues.maxFuel.ToString();
        int points = playerValues.score;
        int fontSize = 72 + 3 * (int)Mathf.Sqrt((float)points);

        pointsText.text = points.ToString();
        pointsText.fontSize = fontSize;
    }
}
