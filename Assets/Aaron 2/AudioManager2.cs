using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager2 : MonoBehaviour
{
    //Tutorial for how to use my AudioManager
    //Create empty object in your scene and name it AudioManager
    //Drag it to the top of the heirarchy
    //Drag this script into the empty object you just created (The AudioManager)
    //In the AudioManager script in the scene you will find a list called Sounds
    //You can add and remove sounds with the + & - or just put a number in
    //drag the sound you want in the Clip and then give it a name example, closing a door may have the name CloseDoor
    //You can choose to loop the sound or not
    //Go into the sript you need for the sound, example: openclosedoors
    //Input the code below where you want the sound to trigger

    //FindObjectOfType<AudioManager2>().Play("AudioName", transform.position); //for playing a sound when something happens

    //OBS! if you cannot hear anything it is likely that you forgot to put the volume and pitch up 
    //If you still cannot hear anything check the name in your code and scene that the are spelled exactly the same, you should get a nullreference message in the console if the names are different 

    [SerializeField] private AudioMixerGroup masterMixerGroup;
    public Sound[] sounds;
    public Sound[] musicTracks;

    public static AudioManager2 instance;
    private AudioSource musicSource; // Dedicated AudioSource for background music

    private Dictionary<string, float> lastPlayedTimes;

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

        DontDestroyOnLoad(gameObject); // Do not destroy between scenes
        lastPlayedTimes = new Dictionary<string, float>();

        foreach (Sound s in sounds)
        {
            try // Added by Neo Ferrari (missing clip caused errors)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = s.minDistance;
                s.source.maxDistance = s.maxDistance;

                if(masterMixerGroup)
                    s.source.outputAudioMixerGroup = masterMixerGroup;

                lastPlayedTimes[s.name] = -s.clip.length; // Initialize with negative clip length to allow immediate play
            }
            catch (Exception e) 
            { 
                Debug.LogException(e);
            }
        }

        // Initialize background music AudioSource
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; // Background music should loop by default
    }

    // Fixed by Neo Ferrari vvv
    public void Play(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"Sound {name} not found!");
            return;
        }

        // Check if the sound was played recently //Neo Ferrari
        if (lastPlayedTimes.TryGetValue(name, out float lastPlayedTime))
        {
            float elapsed = Time.time - lastPlayedTime;
            if (elapsed < s.clip.length * 0.1f)
            {
                // Sound is still playing
                return;
            }
        }

        s.source.transform.position = position;
        s.source.Play();
        lastPlayedTimes[name] = Time.time;
    }

    public void Play(string name, GameObject gameObject)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            if (gameObject.GetComponent<AudioSource>() == null) { gameObject.AddComponent<AudioSource>(); }
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = s.clip;
            audioSource.volume = s.volume;
            audioSource.pitch = s.pitch;
            audioSource.loop = s.loop;
            audioSource.spatialBlend = s.spatialBlend;
            audioSource.minDistance = s.minDistance;
            audioSource.maxDistance = s.maxDistance;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Sound " + name + " not found!");
        }
    }

    //public void SetVolume(string name, float volume)
    //{
    //    Sound s = Array.Find(sounds, sound => sound.name == name);
    //    if (s != null)
    //    {
    //        s.source.volume = volume;
    //    }
    //    else
    //    {
    //        Debug.LogError("Sound " + name + " not found!");
    //    }
    //}

    // Method to play background music
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicTracks, track => track.name == name);
        if (s == null)
        {
            Debug.LogError($"Music track {name} not found!");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.spatialBlend = s.spatialBlend;
        musicSource.minDistance = s.minDistance;
        musicSource.maxDistance = s.maxDistance;
        musicSource.Play();
    }

    // Method to stop background music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Method to set background music volume
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}