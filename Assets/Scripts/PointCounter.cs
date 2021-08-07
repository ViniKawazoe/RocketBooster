using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private Animator pointAddAnimationController;
    [SerializeField] private TextMeshProUGUI pointCounterTMP;
    [SerializeField] private TextMeshProUGUI highscoreTMP;

    [SerializeField] private int points = 0;
    [SerializeField] private int highscore = 0;

    private AudioManager audioManager;

    private const string HIGHSCORE_PREFIX = "HS ";

    #region Singleton
    public static PointCounter Instance;

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
    #endregion

    void Start()
    {
        points = 0;
        SetPointText();
        SetHighscoreText();

        audioManager = AudioManager.Instance;
    }

    public void AddPoint(int amount)
    {
        points += amount;
        pointAddAnimationController.SetTrigger("Trigger");
        SetPointText();
        audioManager.PlayOneShot("AddPointSFX");
    }

    public int GetPoints()
    {
        return points;
    }

    private void SetPointText()
    {
        pointCounterTMP.text = points.ToString();
    }

    public void RestartPoints()
    {
        SetHighscore();
        points = 0;
        SetPointText();
    }

    private void SetHighscore()
    {
        if (points > highscore)
        {
            highscore = points;
            SetHighscoreText();
        }
    }

    private void SetHighscoreText()
    {
        highscoreTMP.text = HIGHSCORE_PREFIX + highscore.ToString();
    }
}
