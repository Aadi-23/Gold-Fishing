using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.ShaderGraph;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer = Time.time % 1.5f;

        if(timer < 0.75f)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.clear;
        }
        else if(timer >= 0.75f)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

    }
}
