using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private SceneData selectedLevel;

    HighScoreManager highScoreManager = new HighScoreManager();
    void Awake()
    {
        highScoreManager.Init();
        highScoreManager.ReadFile();
    }

    public void SelectLevel(int levelIndex)
    {
        if (levelIndex < levelData.sceneData.Length)
        {
            titleText.text = levelData.sceneData[levelIndex].sceneName;
            descriptionText.text = levelData.sceneData[levelIndex].description;
            selectedLevel = levelData.sceneData[levelIndex];
            if (levelIndex > 0)
                highScoreText.text = "High Score: " + highScoreManager.GetScore(levelIndex);
            else highScoreText.text = string.Empty;
        }
        else Deselect();
    }

    public void Deselect()
    {
        titleText.text = string.Empty;
        descriptionText.text = string.Empty;
        highScoreText.text = string.Empty;

        selectedLevel = SceneData.None;
    }

    

    public void PlayLevel()
    {
        selectedLevel.Load();
    }
}
