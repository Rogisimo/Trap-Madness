using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        PlayLoop("MainMusic");
    }


    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.PlayOneShot(s.clip);
    }

    public void PlayLoop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Play();
        s.loop = true;
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Stop();
    }
}
