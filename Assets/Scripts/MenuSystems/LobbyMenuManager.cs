using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyMenuManager : MonoBehaviour
{
    public GameObject[] playerInputPanels;

    public TMP_Text playerNum, killNum, timeLimit;

    public void Start()
    {
        InitializeValues();
    }

    public void ChangePlayerNumber(int num)
    {
        MasterGameManager.instance.numberOfPlayers += num;

        var numberOfPlayers = MasterGameManager.instance.numberOfPlayers;
        playerNum.text = numberOfPlayers.ToString();

        for (int i = 0; i < playerInputPanels.Length; i++)
        {
            if (i < numberOfPlayers)
            {
                playerInputPanels[i].SetActive(true);
                if(i >= MasterGameManager.instance.playersNames.Count)
                {
                    MasterGameManager.instance.playersNames.Add(playerInputPanels[i].GetComponentInChildren<TMP_InputField>().text);
                }
            }
            else
            {
                playerInputPanels[i].SetActive(false);
                if(i < MasterGameManager.instance.playersNames.Count)
                {
                    MasterGameManager.instance.playersNames.Remove(playerInputPanels[i].GetComponentInChildren<TMP_InputField>().text);
                }
            }
        }

    }
    public void ChangeMaxKills(int num)
    {
        MasterGameManager.instance.killLimit += num;
        killNum.text = MasterGameManager.instance.killLimit.ToString();
    }
    public void ChangeTimer(int num)
    {
        MasterGameManager.instance.roundTime += num;
        var time = MasterGameManager.instance.roundTime;

        var minutes = Mathf.Floor(time / 60).ToString("00");
        var seconds = (time % 60).ToString("00");

        timeLimit.text = minutes + ":" + seconds;
    }

    public void UpdatePlayersData(int i)
    {
        MasterGameManager.instance.playersNames[i] = playerInputPanels[i].GetComponentInChildren<TMP_InputField>().text;
    }
    public void LoadScene(string levelName)
    {
        //Save The Settings
        GameData saveData = MasterGameManager.instance.saveData;
        saveData.killCount = MasterGameManager.instance.killLimit;
        saveData.timeLimit = MasterGameManager.instance.roundTime;
        saveData.numberOfPlayers = MasterGameManager.instance.numberOfPlayers;
        foreach(string name in MasterGameManager.instance.playersNames)
        {
            if (!saveData.playerScores.ContainsKey(name))
            {
                saveData.playerScores.Add(name, 0);
            }
        }
        SaveSystem.instance.SaveGame(saveData);
        SceneManager.LoadScene(levelName);
    }
    public void InitializeValues()
    {
        //Display all the characters
        var numberOfPlayers = MasterGameManager.instance.numberOfPlayers;
        playerNum.text = numberOfPlayers.ToString();

        for (int i = 0; i < playerInputPanels.Length; i++)
        {
            if (i < numberOfPlayers)
            {
                playerInputPanels[i].SetActive(true);
                if (i >= MasterGameManager.instance.playersNames.Count)
                {
                    MasterGameManager.instance.playersNames.Add(playerInputPanels[i].GetComponentInChildren<TMP_InputField>().text);
                }
            }
            else
            {
                playerInputPanels[i].SetActive(false);
                if (i < MasterGameManager.instance.playersNames.Count)
                {
                    MasterGameManager.instance.playersNames.Remove(playerInputPanels[i].GetComponentInChildren<TMP_InputField>().text);
                }
            }
        }
        //Display the starting time
        var time = MasterGameManager.instance.roundTime;

        var minutes = Mathf.Floor(time / 60).ToString("00");
        var seconds = (time % 60).ToString("00");

        timeLimit.text = minutes + ":" + seconds;
        //DIsplay kills
        killNum.text = MasterGameManager.instance.killLimit.ToString();
    }
}
