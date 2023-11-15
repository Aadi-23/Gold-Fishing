using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStarter : MonoBehaviour
{
    public float timer = 4;
    public GameObject startOverlay;

    private void Update()
    {

        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
        {
            timer -= Time.deltaTime;
            startOverlay.SetActive(true);
            if(timer>1f)
            {
                startOverlay.GetComponentInChildren<TextMeshProUGUI>().text = ((int)timer).ToString();
            }
            else if (timer <= 1)
            {
                startOverlay.GetComponentInChildren<TextMeshProUGUI>().text = "GO";
            }
        }
        else
        {
            startOverlay.SetActive(false);
            timer = 4;
        }

        if (timer <= 1)
        {
            Changescene();
        }


       
    }
    private void Changescene()
    {
        SceneManager.LoadScene("Game");
    }



}
