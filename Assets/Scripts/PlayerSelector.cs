using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSelector : MonoBehaviour
{
    private List<Transform> players = new List<Transform>();
    private int selectedIndex = 0;
    private int numberOfPlayers;

    public static PlayerSelector Instance { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Player")
            {
                players.Add(child);
            }
        }
        numberOfPlayers = players.Count();
        SetActivePlayer(selectedIndex);
    }

    private void SetActivePlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == selectedIndex)
            {
                players[i].gameObject.SetActive(true);
            }
            else
            {
                players[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickRight()
    {
        if (selectedIndex < numberOfPlayers - 1)
        {
            selectedIndex++;
            SetActivePlayer(selectedIndex);
        }
    }

    public void OnClickLeft()
    {
        if (selectedIndex > 0)
        {
            selectedIndex--;
            SetActivePlayer(selectedIndex);
        }
    }

    public Transform GetSelectedPlayer()
    {
        return players[selectedIndex];
    }

    public bool IsSelectedPlayerUnlocked()
    {
        var child = transform.GetChild(selectedIndex);
        var player = child.GetComponent<Player>();
        return player.IsPlayable();
    }
}