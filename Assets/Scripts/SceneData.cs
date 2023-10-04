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

    [Header("Description")]
    [TextArea(7, 10)]
    public string description;

    public void Load()
    {
        if(sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public static SceneData None
    {
        get
        {
            return new SceneData();
        }
    }
}
