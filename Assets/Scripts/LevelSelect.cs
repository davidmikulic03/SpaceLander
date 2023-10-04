using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private SceneData selectedLevel;

    public void SelectLevel(int levelIndex)
    {
        titleText.text = levelData.sceneData[levelIndex].sceneName;
        descriptionText.text = levelData.sceneData[levelIndex].description;
        selectedLevel = levelData.sceneData[levelIndex];
    }

    public void Deselect()
    {
        titleText.text = "";
        descriptionText.text = "";
        selectedLevel = SceneData.None;
    }

    public void PlayLevel()
    {
        selectedLevel.Load();
    }
}
