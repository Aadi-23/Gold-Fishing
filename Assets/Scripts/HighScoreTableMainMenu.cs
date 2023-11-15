using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreTableMainMenu : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

   
    private List<Transform> highscoreEntryTransformList;

    private Highscores highscores;


    private TextMeshProUGUI scoretxt;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighscoreElements");
        entryTemplate.gameObject.SetActive(false);

        LoadTable();
        
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.L))
        {
            ResetData();
        }
    }

    private void LoadTable()
    {
        string jsonstring = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonstring);

        Debug.Log(jsonstring);

        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        // sorting algorithm(insertion)
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;

                }
            }
        }



        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }
    private void OnDisable()
    {

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry entry, Transform container, List<Transform> transformlist)
    {

        float templateHeight = 45f;
        Transform entryTransform = Instantiate(entryTemplate, container);

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition = new Vector2(0f, -templateHeight * transformlist.Count);

        entryTransform.gameObject.SetActive(true);




        int score = entry.score;

        entryTransform.Find("PointsTxt").GetComponent<TextMeshProUGUI>().text = score.ToString();


        string name = entry.name;
        entryTransform.Find("PlayerNameTxt").GetComponent<TextMeshProUGUI>().text = name;


        transformlist.Add(entryTransform);
    }

  
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
        LoadTable();

        SceneManager.LoadScene("MainMenu");
        
        
    }

    [Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }
}
