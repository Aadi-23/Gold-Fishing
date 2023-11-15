using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainMechanic : MonoBehaviour
{
    public bool Ishidden = false;


    void Update()
    {
        if(Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
        {
            Ishidden = true;
        }
        else
        {
            Ishidden = false;
        }

        if(Ishidden)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
