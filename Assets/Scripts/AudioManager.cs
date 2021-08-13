using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private float defaultVolume = 0.5f;

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerPrefsController.SetMusicVolume(defaultVolume);
            PlayerPrefsController.SetSFXVolume(defaultVolume);

            foreach (Sound sound in sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.volume = GetVolume(sound);
                sound.Source.playOnAwake = sound.PlayOnAwake;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float GetVolume(Sound sound)
    {
        if (sound.IsMusic)
        {
            return PlayerPrefsController.GetMusicVolume();
        }
        else
        {
            return PlayerPrefsController.GetSFXVolume();
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

    public void SetVolume(bool IsMusic, float volume)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.IsMusic == IsMusic)
            {
                sound.Source.volume = volume;
            }
        }
    }
}
