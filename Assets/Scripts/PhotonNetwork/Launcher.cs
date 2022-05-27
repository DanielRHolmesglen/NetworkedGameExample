using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //new
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;
    //new
    [Tooltip("The UI panel to fill in room details once connected")]
    [SerializeField]
    private GameObject connectPanel;
    [SerializeField]
    private TMP_InputField createRoomName, joinRoomName, playerNameField;

    #endregion


    #region Private Fields


    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";
    bool isConnecting;


    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        //new
        connectPanel.SetActive(false);
    }


    #endregion


    #region Public Methods


    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
        // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
       
    }
    public void EnterName()
    {
        PhotonNetwork.NickName = playerNameField.text;
        
        progressLabel.SetActive(false);
        controlPanel.SetActive(false);
        connectPanel.SetActive(true);

    }
    public void Back()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        connectPanel.SetActive(false);
        PhotonNetwork.Disconnect();
    }
    public void ConnectToNamedRoom()
    {
        var name = joinRoomName.text;
        if(name == null)
        {
            Debug.LogError("Tried to join a room but there is no name provided. Enter a name and try again");
        }
        else
        {
            PhotonNetwork.JoinRoom(name);
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            connectPanel.SetActive(false);
        }
        
    }
    public void CreateARoom()
    {
        var name = createRoomName.text;
        if (name == null)
        {
            Debug.LogError("Tried to make a room but there is no name provided. Enter a name and try again");
        }
        else
        {
            PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            connectPanel.SetActive(false);
        }
        
    }
    #endregion
    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
        progressLabel.SetActive(false);
        controlPanel.SetActive(false);
        connectPanel.SetActive(true);

    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        progressLabel.SetActive(false);
        connectPanel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    
    /// <summary>
    /// Use these events and the PhotonNetwor.JoinRandomRoom() functions in another script if you wish delay control of the room.
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        connectPanel.SetActive(false);
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        //PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");


            // #Critical
            // Load the Room Level.
            //PhotonNetwork.LoadLevel("Room for 1");
        }
        //new
        progressLabel.SetActive(false);
        controlPanel.SetActive(false);
        connectPanel.SetActive(false);
        PhotonNetwork.LoadLevel("Room for 1");
    }

    #endregion
}