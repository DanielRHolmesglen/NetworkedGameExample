using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleGameMaster : MonoBehaviour
{
    public ExampleGameData saveData = new ExampleGameData();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveData.AddScore(1);
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveData.AddScore(-1);
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ExampleSaveSystem.instance.SaveGame(saveData);
            Debug.Log("Saved data.");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            saveData = ExampleSaveSystem.instance.LoadGame();
            Debug.Log("Loaded data.");
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            saveData.ResetData();
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintPlayerScore();
        }
    }

    void PrintScore()
    {
        Debug.Log("The current score is " + saveData.score);
        
        
    }
    void PrintPlayerScore()
    {
        List<KeyValuePair<string, int>> orderedScores = saveData.playerScores.ToList();
        int Compare2(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }
        orderedScores.Sort(Compare2);
        foreach (KeyValuePair<string, int> pair in orderedScores)
        {
            Debug.Log(pair.Key + " has " + pair.Value + " points");
        }
    }
}
