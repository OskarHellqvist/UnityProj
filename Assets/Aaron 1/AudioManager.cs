using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //FindObjectOfType<AudioManager>().Play("AudioName"); //for playing a sound when something happens

    
    public static AudioManager instance;

    void Awake()
    {
        //Destroy if AudioManager duplicates
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Do not destroy between scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        
    }

    public void Play (string name)
    {
        // finds sound in the sound array
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
