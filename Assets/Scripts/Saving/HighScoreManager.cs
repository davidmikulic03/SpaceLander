using System.IO;
using UnityEngine;

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
            File.Create(savePath).Close();
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
