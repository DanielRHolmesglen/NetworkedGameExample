using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; //needed for sendevent
public class RaiseEventExample : MonoBehaviourPun
{
    private SpriteRenderer _spriteRenderer;
    PhotonView pv;

    private const byte COLOR_CHANGE_EVENT = 0;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!pv.IsMine) return;
        if (Input.GetKeyDown(KeyCode.V)) ChangeColor();
    }
    private void ChangeColor() //the function to be called
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);


        _spriteRenderer.color = new Color(r, g, b);

        object[] datas = new object[] { r, g, b }; //raising the event

        PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }

    #region Subscribe and Unsubscribe to Events
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if(obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            float r = (float)datas[0];
            float g = (float)datas[1];
            float b = (float)datas[2];
            _spriteRenderer.color = new Color(r, g, b);
        }
    }
#endregion
}
