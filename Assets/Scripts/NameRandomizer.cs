using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NameRandomizer : MonoBehaviour
{
    private Names names;

    private TextMeshProUGUI nametxt;
    private TextMeshProUGUI scoreTxt;

    void Start()
    {
        nametxt = GameObject.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        scoreTxt = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();


        scoreTxt.text = "Your Score : " + PlayerPrefs.GetInt("Score");

        string jsonstring = PlayerPrefs.GetString("NamesList");
        names = JsonUtility.FromJson<Names>(jsonstring);

        if (names == null)
        {
            names = new Names
            {
                NameEntryList = new List<NameEntry>()
                {
                    new NameEntry { Adjective = "Mighty", Noun = "Warrior" },
                    new NameEntry { Adjective = "Sneaky" , Noun = "Theif" },
                    new NameEntry { Adjective = "Brave" , Noun = "Knight" },
                    new NameEntry { Adjective = "Cloaked" , Noun = "Mage" },
                    new NameEntry { Adjective = "Bait" , Noun = "Master" },
                    new NameEntry { Adjective = "Invisible", Noun = "Goblin" },
                    new NameEntry { Adjective = "Royal", Noun = "Fisher" },
                    new NameEntry { Adjective = "Burned", Noun = "Angler"  },
                    new NameEntry { Adjective = "Greedy" , Noun = "Slayer" },
                    new NameEntry { Adjective = "Bright" , Noun = "Bandit"},
                    new NameEntry { Adjective = "Cool" , Noun = "Caster"},
                    new NameEntry { Adjective = "Happy" , Noun = "Fighter"},
                }
            };
        }


        RandomizeName();

        StartCoroutine(ChangeScene(6f));
    }

    

    public void RandomizeName()
    {
        int adjectiveindex = Random.Range(0, 10);
        int nounindex = Random.Range(0, 10);

        string randomizedname = names.NameEntryList[adjectiveindex].Adjective + " " + names.NameEntryList[nounindex].Noun;
        nametxt.text = randomizedname; 

        PlayerPrefs.SetString("Playername", randomizedname);
    }


    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Lose");
    }


    [Serializable]
    private class Names
    {
        public List<NameEntry> NameEntryList;
    }

    [System.Serializable]
    private class NameEntry
    {
        public string Adjective;
        public string Noun;
    }
}
