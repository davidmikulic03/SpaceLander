using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct SceneData
{
    [Header("Scene")]
    public string sceneName;

    [Header("Player Values")]
    public float fuelBurnRate;
    public float maxFuel;
    public float maxHealth;
    public float initialFuel;
    public float initialHealth;

    [Header("Description")]
    [TextArea(7, 10)]
    public string description;

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
