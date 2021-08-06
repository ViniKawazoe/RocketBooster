using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
        }
    }

    public void PlayAudio(string audioName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.Name == audioName)
            {
                sound.Source.Play();
            }
        }
    }
}
