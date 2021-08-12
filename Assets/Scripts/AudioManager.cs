using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (Sound sound in sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.volume = sound.Volume;
                sound.Source.playOnAwake = sound.PlayOnAwake;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(string audioName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.Name == audioName)
            {
                if (!sound.Source.isPlaying)
                {
                    sound.Source.PlayOneShot(sound.Clip);
                }
            }
        }
    }

    public void Stop(string audioName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.Name == audioName)
            {
                if (sound.Source.isPlaying)
                {
                    sound.Source.Stop();
                }
            }
        }
    }

    public void ChangeVolume(bool IsMusic, float volume)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.IsMusic == IsMusic)
            {
                sound.Source.volume = volume;
            }
        }
    }

    public List<Sound> GetSounds()
    {
        List<Sound> sounds = new List<Sound>();
        foreach (Sound sound in sounds)
        {
            sounds.Add(sound);
        }
        return sounds;
    }
}
