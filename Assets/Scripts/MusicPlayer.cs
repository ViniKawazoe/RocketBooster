using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private string musicName;

    private AudioManager audioManager;

    public static MusicPlayer Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.Play(musicName);
        }
    }
}
