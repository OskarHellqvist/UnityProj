using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
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
    //FindObjectOfType<AudioManager>().Play("AudioName"); //for playing a sound when something happens

    //OBS! if you cannot hear anything it is likely that you forgot to put the volume and pitch up 
    //If you still cannot hear anything check the name in your code and scene that the are spelled exactly the same, you should get a nullreference message in the console if the names are different 

    public Sound[] sounds;
    
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

    public void Play (string name)
    {
        // finds sound in the sound array
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

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
            Debug.LogWarning("Sound " + name + " not found!");
        }
    }
}

