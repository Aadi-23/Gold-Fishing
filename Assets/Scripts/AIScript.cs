using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//using PlasticPipe.PlasticProtocol.Messages;
//using UnityEditor.MemoryProfiler;
//using static Codice.CM.Common.CmCallContext;
using System.IO.Ports;
using System.IO;





public class AIScript : MonoBehaviour
{
    //GameObject Connection;
    SerialPort stream = new SerialPort("COM3", 9600);
    public bool arduinoConnected = true;

    private CurtainMechanic curtain;

    private bool PlayerDead = false;

    public Sprite dragonsleep;
    public Sprite dragonwaking;
    public Sprite dragonawake;


    private Animator dragonanime;

    int sound = 0;

    float stateTimer = 0;
    float detectionTimer = 0;

    //Every state of the dragon
    enum dragonstate
    {
        SLEEP,
        WAKING,
        WAK_AWAKE_TRANS,
        AWAKE,
        ANGRY
    }
    
    dragonstate currentstate;
    
    // This function changes the dragon's state
    private void ChangeState(dragonstate newState)
    {
        ExitState(currentstate);
        currentstate= newState;
        EnterState(currentstate);
    }
    
    // When a new state is initialized, specific code is executed depending on state
    private void EnterState(dragonstate current)
    {
        //Connection.GetComponent<ArduinoComms>().stream.WriteLine(current.ToString());

        switch (current)
        {
            case dragonstate.SLEEP:

                
                stateTimer = Random.Range(16, 22);

                this.gameObject.GetComponent<SpriteRenderer>().sprite = dragonsleep;
                
                //Start playing random sound
                sound = Random.Range(1,4);
                switch(sound)
                {
                    case 1:
                        SoundManager.Instance.Play("Dragon Sleeping1");
                        Debug.Log("playing 1");
                        break;
                    case 2:
                        SoundManager.Instance.Play("Dragon Sleeping2");
                        Debug.Log("playing 2");
                        break;
                    case 3:
                        SoundManager.Instance.Play("Dragon Sleeping3");
                        Debug.Log("playing 3");
                        break;
                }
                if(arduinoConnected)
                    stream.WriteLine(1.ToString());
                break;

            case dragonstate.WAKING:

                stateTimer = Random.Range(8, 10);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = dragonwaking;
                SoundManager.Instance.Oneshot("Dragon Sleepy");
                if (arduinoConnected)
                    stream.WriteLine(2.ToString());
                break;

            case dragonstate.WAK_AWAKE_TRANS:

                stateTimer = 0.6f;
                break;

            case dragonstate.AWAKE:
                
                stateTimer = Random.Range(4,6);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = dragonawake;
                if (arduinoConnected)
                    stream.WriteLine(3.ToString());
                break;

            case dragonstate.ANGRY:
                
                stateTimer = 1.2f;
                if (arduinoConnected)
                    stream.WriteLine(4.ToString());
                break;
        }
    }

    // When a state is exited, specific code is executed
    private void ExitState(dragonstate current)
    {

        switch (current)
        {
            case dragonstate.SLEEP:
                
                switch (sound)
                {

                    case 1:
                        SoundManager.Instance.Stop("Dragon Sleeping1");
                        Debug.Log("stopping 1");
                        break;
                    case 2:
                        SoundManager.Instance.Stop("Dragon Sleeping2");
                        Debug.Log("stopping 2");
                        break;
                    case 3:
                        SoundManager.Instance.Stop("Dragon Sleeping3");
                        Debug.Log("stopping 3");
                        break;
                }
                break;

            case dragonstate.WAKING:

                
                break;

            case dragonstate.WAK_AWAKE_TRANS:


                break;

            case dragonstate.AWAKE:

               
                break;

            case dragonstate.ANGRY:
                break;
        }
    }

    void Start()
    {

        dragonanime = GetComponent<Animator>();

       
        try
        {
        stream.Open();
        }
        catch(IOException)
        {
            Debug.Log("Arduino not connected");
            arduinoConnected= false;
        }
        // Finds the script
        curtain = GameObject.FindObjectOfType<CurtainMechanic>();
        // Initialize first state
        EnterState(dragonstate.SLEEP);
    }

    // Different update logic is executed depending on the current state
    void Update()
    {
        //Connection.GetComponent<ArduinoComms>().stream.WriteLine(0.ToString());

        switch (currentstate) 
        {
            case dragonstate.SLEEP:

               
                dragonanime.SetTrigger("Sleep");
                stateTimer -= Time.deltaTime;
                if(stateTimer <= 0)
                {
                    ChangeState(dragonstate.WAKING);
                }
                break;

            case dragonstate.WAKING:

                dragonanime.SetTrigger("HalfAwake");
                stateTimer -= Time.deltaTime;
                
                if (stateTimer <= 0)
                {
                    float random = Random.value;
                    Debug.Log(random);
                    if (random < 0.6)
                    {
                        ChangeState(dragonstate.WAK_AWAKE_TRANS);
                    }
                    else
                    {
                        ChangeState(dragonstate.SLEEP);
                    }
                }
                break;

            case dragonstate.WAK_AWAKE_TRANS:
                dragonanime.SetTrigger("Awake");
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    ChangeState(dragonstate.AWAKE);
                }
                break;

            case dragonstate.AWAKE:

                //dragonanime.SetTrigger("Awake");
                if (!curtain.Ishidden && !PlayerDead)
                {
                    detectionTimer += Time.deltaTime;
                    
                    if(detectionTimer >= 0.5 && stateTimer >= 0.6)
                    {
                    PlayerDead = true;
                    SoundManager.Instance.Oneshot("Dragon Roar");
                    ChangeState(dragonstate.ANGRY);
                    }
                }
                else if(curtain.Ishidden)
                {
                    detectionTimer = 0;
                }

                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    ChangeState(dragonstate.SLEEP);
                }


                if (stateTimer <= 0.5)
                {
                    dragonanime.SetTrigger("GoSleep");
                }
                break;

            case dragonstate.ANGRY:

                dragonanime.SetTrigger("Attack");
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    SceneManager.LoadScene("NamingTransition");
                }
                break;
        }

    }
}
