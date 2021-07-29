using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointCounterTMP;
    [SerializeField] private TextMeshProUGUI highscoreTMP;

    [SerializeField] private int points = 0;
    [SerializeField] private int highscore = 0;

    private const string highscorePrefix = "HS ";

    #region Singleton
    void Awake()
    {
        int objectsInScene = FindObjectsOfType<PointCounter>().Length;

        if (objectsInScene > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    void Start()
    {
        points = 0;
        SetPointText();
        SetHighscoreText();
    }

    public void AddPoint(int amount)
    {
        points += amount;
        SetPointText();
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
        highscoreTMP.text = highscorePrefix + highscore.ToString();
    }
}
