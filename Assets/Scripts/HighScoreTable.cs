using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private Highscores highscores;


    private TextMeshProUGUI scoretxt;

    private int score;

    private string playername;

    
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreContainer");
       
        entryTemplate = entryContainer.Find("HighscoreElements");


        entryTemplate.gameObject.SetActive(false);

       

        score = PlayerPrefs.GetInt("Score");
        playername = PlayerPrefs.GetString("Playername");

        




        LoadTable();


       
        


    }

    private void LoadTable()
    {
        string jsonstring = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonstring);

        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }








        AddingHighscoreEntry(score, playername);




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

        float templateHeight = 50f;
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

    private void AddingHighscoreEntry(int scores , string names)
    {
        HighscoreEntry highscoreentry = new HighscoreEntry { score = scores, name = names };

        

        highscores.highscoreEntryList.Add(highscoreentry);

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

        if(highscores.highscoreEntryList.Count > 5)
        {
            for(int i = highscores.highscoreEntryList.Count; i > 5; i--)
            {
                highscores.highscoreEntryList.RemoveAt(5);
            }
        }

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("highscoreTable"));

    }

    [Serializable]
    private class Highscores
    {
        public List<HighscoreEntry>highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

    }
}
