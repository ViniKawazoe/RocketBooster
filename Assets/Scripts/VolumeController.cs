using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioManager audioManager;
    private Slider slider;
    private bool isMusic;
    private float sliderVolume = 0.5f;

    void Awake()
    {
        audioManager = AudioManager.Instance;
        slider = gameObject.GetComponent<Slider>();
        isMusic = gameObject.name.Contains("Music");
    }

    void Start()
    {
        if (audioManager != null)
        {
            List<Sound> sounds = audioManager.GetSounds();
            List<Sound> validSounds = sounds.Where(x => x.IsMusic == isMusic).ToList();
            float volume = validSounds.Select(x => x.Source.volume).FirstOrDefault();
            slider.value = volume;
        }
    }

    public void OnDrag()
    {
        if (audioManager == null) { return; }
        sliderVolume = slider.value;
        audioManager.ChangeVolume(isMusic, sliderVolume);
    }
}
