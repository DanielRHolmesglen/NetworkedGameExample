using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    

    /// <summary>
    /// game settings
    /// </summary>
    public int timeLimit = 60;
    public int numberOfPlayers = 2;
    public int killCount = 5;
    public Dictionary<string, int> playerScores = new Dictionary<string, int>();

    public void AddPlayerScore(string name, int score)
    {
        playerScores.Add(name, score);
    }

    public void ShowScores()
    {
        Debug.Log(playerScores);
    }
}
