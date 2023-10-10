using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[Serializable]
public class HighScores
{
    public int[] highScores = new int[5];

    public void AssignScore(int level, int score)
    {
        if (highScores[level] < score) highScores[level] = score;
    }

    public static HighScores Empty
    {
        get
        {
            return new HighScores();
        }
    }
}
