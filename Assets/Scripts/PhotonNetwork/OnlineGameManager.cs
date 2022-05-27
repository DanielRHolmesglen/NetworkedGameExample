using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;
    public static List<GameObject> players = new List<GameObject>();
    public int MaxPlayerCount = 4;
    public TMP_Text playerNameList;
    bool isFull;
        #region Photon Callbacks


        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Launcher");
        }


        #endregion


        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


    #endregion
    #region Private Methods
    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            //new
            var player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            //player.GetComponentInChildren<TMP_Text>().text = PhotonNetwork.LocalPlayer.ActorNumber + " " + PhotonNetwork.NickName;
            players.Add(player);

            

            UpdatePlayerListUI();
        }
    }
    void UpdatePlayerListUI()
    {
        string playerList = "";
        foreach(GameObject player in players)
        {
            playerList += player.GetComponent<PhotonView>().Owner + "\n";
        }
        playerNameList.text = playerList;
    }
    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        if (isFull)
        {
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
        else
        {
            Debug.Log("Attempted to load scene but there is not enough players");
        }
        
    }


    #endregion
    #region Photon Callbacks

   
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        playerNameList.text = PhotonNetwork.PlayerList.ToString();

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            
            if(PhotonNetwork.CountOfPlayers == MaxPlayerCount)
            {
                isFull = true;
            }
            else if (PhotonNetwork.CountOfPlayers < MaxPlayerCount)
            {
                isFull = false;
            }
        }
        UpdatePlayerListUI();
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            if (PhotonNetwork.CountOfPlayers == MaxPlayerCount)
            {
                isFull = true;
            }
            else if (PhotonNetwork.CountOfPlayers < MaxPlayerCount)
            {
                isFull = false;
            }
        }
    }


    #endregion
}
