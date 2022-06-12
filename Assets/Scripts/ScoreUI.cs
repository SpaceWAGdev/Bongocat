using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text highScore;
    [SerializeField] TMPro.TMP_Text totalScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        totalScore.text = PlayerPrefs.GetInt("TotalScore").ToString();
    }
}
