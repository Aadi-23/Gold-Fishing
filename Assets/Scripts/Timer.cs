using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float timer = 60;

    public TextMeshProUGUI timertext;

   // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;      // Updates timer with real time


        timertext.text = timer.ToString("0");

        // Checks for what to do when time ends
        if(timer <= 0)
        {
           SceneManager.LoadScene("NamingTransition");
        }
    }
}
