using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    //SINGLETON PATTERN
    public static RoundManager instance;
    private void Awake()
    {
        #region Singleton pattern
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }
    //list of players
    public List<GameObject> players;
    //list of scores
    public int[] playerScores;
    //list of spawn positions
    public List<Transform> spawnPositions;

    public int maxKills, roundTime;
    //list of scripts for the game manager to reference
    [SerializeField] RoundUIManager UIManager;
    public void Start()
    {
        SetupScene();
        StartCoroutine(Timer(roundTime));
    }
    //SETUP SCENE
    //check that all required scripts and prefabs are in the scene. Set up play area, and reset all variables for a new round
    //run the spawn players function for each player
    public void SetupScene()
    {
        var playerNumber = MasterGameManager.instance.numberOfPlayers;
        roundTime = MasterGameManager.instance.roundTime;
        maxKills = MasterGameManager.instance.killLimit;

        playerScores = new int[playerNumber];

        for(int i = 0; i < playerNumber; i++)
        {
            if(i >= players.Count)
            {
                players.Add(players[0]);
            }
        }
        for (int i = 1; i < players.Count+1; i++)
        {
            SpawnPlayer(i);
        }
        UIManager.UpdateScoreUI();
    }
    //SPAWN PLAYERS
    //takes in a player, finds a random position from the list, and spawns the player in that location.
    public void SpawnPlayer(int playerNumber)
    {
        var spawnPoint = new Vector3(Random.Range(spawnPositions[0].position.x, spawnPositions[1].position.x), 0, Random.Range(spawnPositions[0].position.z, spawnPositions[1].position.z));
        var player = Instantiate(players[playerNumber-1], spawnPoint, players[playerNumber-1].transform.rotation);

        var playerInputs = player.GetComponent<PlayerInputs>();
        playerInputs.playerNum = playerNumber;
        playerInputs.playerName = MasterGameManager.instance.playersNames[playerNumber - 1];
        playerInputs.DetermineInputs();
        playerInputs.UpdateUI();


        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.canBeDamaged = false;
        playerHealth.Invoke("ResetDamage", 2f);

    }
    //UPDATE SCORE
    // increments or decrements the score of a player, then checks all player scores to see if someone has won.
    // triggers the end round and passes information on. also calls out to the UI manager if it exists to update.
    public void UpdateScore(int playerScoring, int playerKilled)
    {
        if (playerScoring == 0 || playerScoring == playerKilled)
        {
            playerScores[playerKilled - 1]--;
        }
        else
        {
            playerScores[playerScoring - 1]++;
        }
        if(UIManager != null) UIManager.UpdateScoreUI();
    }
    //RUN TIMER
    //ticks down the timer and checks for end round. passes info to UI manager if it exists
    public IEnumerator Timer(int time)
    {
        WaitForSeconds WFS = new WaitForSeconds(1f);
        while (time > 0)
        {
            time--;
            UIManager.DisplayTimer(time);
            yield  return WFS;
        }
        EndRound();
    }
    //END ROUND
    // calls an end to the round, triggers any end round events. Most likely this will pass of to another script/object that
    // handles score displays.
    public void EndRound()
    {
        var WinningPlayer = 0;
        for(int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] > WinningPlayer) WinningPlayer = i;
        }
        Debug.Log("Game Over! Player " + WinningPlayer + " Has won the game!");
        if (UIManager != null) UIManager.DisplayResults(WinningPlayer + 1);
    }
}
