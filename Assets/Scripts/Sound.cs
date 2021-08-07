using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0, 2f)]
    public float Volume;
    public bool Loop;
    public bool PlayOnAwake;

    [HideInInspector]
    public AudioSource Source;
}
