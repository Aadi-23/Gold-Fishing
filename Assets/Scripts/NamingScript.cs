using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NamingScript : MonoBehaviour
{

    private TMP_InputField playernametxt;

    private TextMeshProUGUI scoreTxt;


    private void Awake()
    {
        playernametxt = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
        scoreTxt = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();


        scoreTxt.text = "Your Score : " + PlayerPrefs.GetInt("Score");

        playernametxt.characterLimit = 10;
    }
    public void ChangeScene()
    {
        if (string.IsNullOrEmpty(playernametxt.text))
        {
            PlayerPrefs.SetString("Playername", "No Name");
            PlayerPrefs.Save();

            SceneManager.LoadScene("Lose");


        }
        else
        {
            PlayerPrefs.SetString("Playername", playernametxt.text);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Lose");
        }
    }

}
