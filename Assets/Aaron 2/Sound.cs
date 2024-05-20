using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Sound 
{
    public string name;
    public AudioClip clip;

    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend = 1f; // 1 for full 3D sound
    public float minDistance = 1f;
    public float maxDistance = 500f;

    [HideInInspector]
    public AudioSource source;
}
