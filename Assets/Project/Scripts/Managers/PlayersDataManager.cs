using System.Collections.Generic;
using UnityEngine;

public class PlayersDataManager : MonoBehaviour
{
    public static PlayersDataManager Instance;

    public Dictionary<int, string> playersName = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        playersName.Clear();
    }

    public void SaveName(int playerNum, string name)
    {
        playersName.Add(playerNum, name);
    }

    public void RemoveName(int playerNum)
    {
        playersName.Remove(playerNum);
    }
}