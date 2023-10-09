using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] Slider cameraSensitivity;
    [SerializeField] Slider scrollSensitivity;

    public void Save()
    {
        PlayerPrefs.SetFloat("Camera Sensitivity", cameraSensitivity.value);
        PlayerPrefs.SetFloat("Scroll Sensitivity", scrollSensitivity.value);
    }

    public void Load()
    {
        cameraSensitivity.value = PlayerPrefs.GetFloat("Camera Sensitivity");
        scrollSensitivity.value = PlayerPrefs.GetFloat("Scroll Sensitivity");
    }
}
