using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{

    private static Score Instance;
    private int score = 0;

    private int Highscore;



    public TextMeshProUGUI scoretext;

    private Timer gametimer;

    private Dictionary<string, float> ScoreVariables;


    private float Timer;


    private const float MAX_TIME = 120;
    private const int BASE_VALUE = 4000;
    private const float TIME_BONUS = 2000;

    void Awake()
    {
        ScoreVariables = new Dictionary<string, float>();
    }

    

    private void OnEnable()
    {
        
        scoretext = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
       
        gametimer = GameObject.FindObjectOfType<Timer>();

        // prints the score at start of the scene
       
        scoretext.text = "Score : " + score;
    }

    private void OnDisable()
    {
        SaveData();
    }
    private void SaveData()
    {
      
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }



    public void KeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {   
            ScoreVariables.Add("Key1", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            ScoreVariables.Remove("Key1");
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            ScoreVariables.Add("Key2", ScoreCalculator(gametimer.timer));

        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            ScoreVariables.Remove("Key2");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ScoreVariables.Add("Key3", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            ScoreVariables.Remove("Key3");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ScoreVariables.Add("Key4", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            ScoreVariables.Remove("Key4");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ScoreVariables.Add("Key5", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ScoreVariables.Remove("Key5");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ScoreVariables.Add("Key6", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ScoreVariables.Remove("Key6");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ScoreVariables.Add("Key7", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            ScoreVariables.Remove("Key7");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ScoreVariables.Add("Key8", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ScoreVariables.Remove("Key8");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ScoreVariables.Add("Key9", ScoreCalculator(gametimer.timer));
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            ScoreVariables.Remove("Key9");
        }
    }


    private int ScoreCalculator(float time)
    {
        int finalscore;

        finalscore = BASE_VALUE + (int)(TIME_BONUS *  (time / MAX_TIME));

        return finalscore;
        
    }
    void Update()
    {

        KeyInputs();

        if(ScoreVariables.Count >= 9)
        {
            Timer += Time.deltaTime;
        }
       


        if(Timer > 1)
        {
            ChangeScene();
        }

        score = (int)ScoreVariables.Sum(x => x.Value);

        scoretext.text = "SCORE : " + score;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("NamingTransition");
    }
}
