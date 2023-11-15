using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public float timer;

    public GameObject tutorial;





    private void Start()
    {
        timer = 0;
    }
    
        

        
    
    public void Update()
    {
        
        
        if (!Input.GetKeyUp(KeyCode.I) && !Input.GetKey(KeyCode.J))
        {
            timer += Time.deltaTime;

        }

        if (timer >= 10f && !tutorial.activeInHierarchy)
        {
            tutorial.SetActive(true);
            timer = 0;
        }

        if(timer > 31.7f)
        {
            tutorial.SetActive(false);
            timer = 0;
        }


        if(Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
        {
            timer = 0;
            
            tutorial.SetActive(false);
            
        }
    }
}
