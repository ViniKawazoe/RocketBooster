using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0, 1f)]
    public float Volume;
    public bool Loop;
    public bool PlayOnAwake;
    public bool IsMusic;

    [HideInInspector]
    public AudioSource Source;
}
