using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalScoreManager
{
    public static void Setup()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        if (!PlayerPrefs.HasKey("TotalScore"))
        {
            PlayerPrefs.SetInt("TotalScore", 0);
        }
    }
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    public static int GetTotalScore()
    {
        return PlayerPrefs.GetInt("TotalScore");
    }
    public static bool NewHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void AddToTotalScore(int score)
    {
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + score);
    }
}
