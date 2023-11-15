using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderBoardUI : MonoBehaviour
{

    private int score;
    private int Highscore;

    private TextMeshProUGUI scoretext;
    private TextMeshProUGUI highscoretext;
   

    private void OnEnable()
    {
        LoadData();
        scoretext = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        highscoretext = GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>();
       

        // prints the score at start of the scene
        highscoretext.text = "High Score : " + Highscore;
        scoretext.text = "Score : " + score;
    }

    private void LoadData()
    {
        Highscore = PlayerPrefs.GetInt("HighScore");
        score = PlayerPrefs.GetInt("Score");
    }

  
}
