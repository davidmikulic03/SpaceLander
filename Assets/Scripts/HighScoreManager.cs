using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HighScoreManager 
{
    public HighScores highScoreClass = new HighScores();

    public void Init()
    {
        ReadFile();
    }

    public void ReadFile()
    {
        string savePath = Application.persistentDataPath + "/gamedata.json";

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            if(json != string.Empty)
            {
                highScoreClass = JsonUtility.FromJson<HighScores>(json);
                return;
            }

            highScoreClass = new HighScores();
            WriteFile();
        }
        else
        {
            File.Create(savePath);
        }
    }

    public void WriteFile()
    {
        string savePath = Application.persistentDataPath + "/gamedata.json";

        string json = JsonUtility.ToJson(highScoreClass);
        File.WriteAllText(savePath, json);
    }

    public int GetScore(int level)
    {
        return highScoreClass.highScores[level];
    }
}
