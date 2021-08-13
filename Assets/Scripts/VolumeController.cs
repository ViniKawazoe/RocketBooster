using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    private AudioManager audioManager;
    private Slider slider;
    private bool isMusic;
    private float sliderVolume = 0.5f;

    void Start()
    {
        audioManager = AudioManager.Instance;
        slider = gameObject.GetComponent<Slider>();
        isMusic = gameObject.name.Contains("Music");

        slider.value = GetVolume();
    }

    private float GetVolume()
    {
        if (isMusic)
        {
            return PlayerPrefsController.GetMusicVolume();
        }
        else
        {
            return PlayerPrefsController.GetSFXVolume();
        }
    }

    void Update()
    {
        audioManager.SetVolume(isMusic, sliderVolume);
    }

    public void OnDrag()
    {
        sliderVolume = slider.value;
        if (isMusic)
        {
            PlayerPrefsController.SetMusicVolume(sliderVolume);
        }
        else
        {
            PlayerPrefsController.SetSFXVolume(sliderVolume);
        }
    }
}
