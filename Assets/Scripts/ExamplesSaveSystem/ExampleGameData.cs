using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExampleGameData
{
  public int score = 0;
    //added by me
    public List<string> unlockedLevels;
    public Dictionary<string, int> playerScores = new Dictionary<string, int>();
    

  public void AddScore(int points)
  {
    score += points;
  }

  public void ResetData()
  {
    score = 0;
  }
    public void UnlockLevle(string level)
    {
        unlockedLevels.Add(level);
    }
    
}
