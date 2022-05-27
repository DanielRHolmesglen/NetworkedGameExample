using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreAdder : MonoBehaviour
{
    public InputField pName, pScore;
    public ExampleGameMaster gm;
    public void AddPlayerScore()
    {
        gm.saveData.playerScores.Add(pName.text, int.Parse(pScore.text));
    }
}
