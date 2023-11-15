using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("SoundManager.Instance.Play(NameOfTheSound)")]
    public Sound[] sounds;
    public static SoundManager Instance;
    public delegate void afterSoundCallback();
    private List<Coroutine> coroutines = new List<Coroutine>();

    public static float generalVolume = 1f;
    public static float effectsVolume = 1f;
    public static float musicVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this){

            Destroy(gameObject);
            return;
        }
        else{
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        foreach (Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume * generalVolume * effectsVolume;
            sound.source.pitch = sound.pitch;
            if(sound.source.pitch < 0.1){
                sound.source.pitch = 1;
            }
            sound.source.loop = sound.loop;
        }

        SceneManager.activeSceneChanged += ClearSoundsOnSceneChange;
    }

    void RefreshSources(){
        Debug.Log("A AudioSource was null, refreshing them!");
        foreach (Sound sound in sounds){
            if(sound.source == null) {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = sound.volume * generalVolume * effectsVolume;
                sound.source.pitch = 1;
                sound.source.loop = sound.loop;
            }
        }
    }

    //Clear all the sounds when chanigin scenes
    void ClearSoundsOnSceneChange(Scene current, Scene next){
        foreach (Coroutine c in coroutines){
            StopCoroutine(c);
        }
        coroutines.Clear();
    }

    //Play sound (more efficient that one shot but re-starts same sound queues)
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log("Playing Sound " + name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return;
        }
        if (s.source == null) RefreshSources();
        s.source.Play();
    }

    //Same but lets you set the volume on the other script
    public void Play(string name, float volume){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log("Playing Sound " + name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return;
        }
        
        s.source.volume = volume * generalVolume * effectsVolume;
        s.source.Play();
    }

    //Plays multiple same of the same sound at the same time
    public void Oneshot(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log("Playing Sound " + name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }

    //Stop playing sound
    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log("No longer playing Sound " + name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return;
        }
        if (s.source == null) return;
        s.source.Stop();
    }

    //Returns length of a sound
    public float Length(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("Getting length of " + name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return 0f;
        }
        return s.clip.length;
    }

    //Check if sound is already playing
    public bool IsPlaying(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound "+ name + " not found.");
            return false;
        }
        if (s.source == null) RefreshSources();
        return s.source.isPlaying;
    }
}
