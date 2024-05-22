using UnityEngine.Audio;
using System;
using UnityEngine;

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

    public Sound[] sounds;
    
    public static AudioManager2 instance;

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

            s.source.spatialBlend = s.spatialBlend;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
        }
    }

    public void Play(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            GameObject soundObject = new GameObject("Sound_" + name);
            soundObject.transform.position = position;
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = s.clip;
            audioSource.volume = s.volume;
            audioSource.pitch = s.pitch;
            audioSource.loop = s.loop;
            audioSource.spatialBlend = s.spatialBlend;
            audioSource.minDistance = s.minDistance;
            audioSource.maxDistance = s.maxDistance;
            audioSource.Play();
            Destroy(soundObject, s.clip.length);
        }
        else
        {
            Debug.LogError("Sound " + name + " not found!");
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = volume;
        }
        else
        {
            Debug.LogError("Sound " + name + " not found!");
        }
    }
}

