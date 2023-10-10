using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI pointsText;

    void Update()
    {
        float displayFuel =  MathF.Round(playerValues.currentFuel, 1);
        float displayHealth = MathF.Round(playerValues.currentHealth, 1);
        fuelText.text = "Fuel : " + displayFuel.ToString() + " / " + playerValues.maxFuel.ToString();
        healthText.text = "Health : " + displayHealth.ToString() + " / " + playerValues.maxHealth.ToString();

        int points = playerValues.score;
        int fontSize = 72 + 3 * (int)Mathf.Sqrt((float)points);
        fontSize = Mathf.Clamp(fontSize, 72, 250);

        pointsText.text = points.ToString();
        pointsText.fontSize = fontSize;
    }
}
