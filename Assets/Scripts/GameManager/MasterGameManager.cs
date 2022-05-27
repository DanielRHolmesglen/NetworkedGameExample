using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterGameManager : MonoBehaviour
{
    //Set up a signleton
    public static MasterGameManager instance;
    private void Awake()
    {
        #region Singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        #endregion
    }
    //get a blank save file. This can be overriden by loading 
    public GameData saveData;// = new GameData();

    public string LevelToPlay;
    public List<string> playersNames = new List<string>();
    public int numberOfPlayers = 2;
    public int roundTime = 60;
    public int killLimit = 10;

    public void Start()
    {
        saveData = SaveSystem.instance.LoadGame();

        numberOfPlayers = saveData.numberOfPlayers;
        roundTime = saveData.timeLimit;
        killLimit = saveData.killCount;

        foreach(KeyValuePair<string, int> name in saveData.playerScores)
        {
            if(playersNames.Count < numberOfPlayers)
            {
                playersNames.Add(name.Key);
            }
        }
    }
}
