using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class OnlineTimer : MonoBehaviour
{
    public int maxTime;
    int timeInSeconds;
    [SerializeField] TMP_Text timerText;
    [SerializeField] PhotonView pv;
    private const byte TIMER_TICK = 1;
    // Start is called before the first frame update
    void Start()
    {
        if(pv.IsMine) StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        timeInSeconds = maxTime;

        var minutes = Mathf.Floor(timeInSeconds / 60).ToString("00");
        var seconds = (timeInSeconds % 60).ToString("00");
        timerText.text = minutes + " : " + seconds;

        while (timeInSeconds > 0)
        {
            timeInSeconds -= 1;

            minutes = Mathf.Floor(timeInSeconds / 60).ToString("00");
            seconds = (timeInSeconds % 60).ToString("00");
            timerText.text = minutes + " : " + seconds;

            object[] data = new object[] { minutes, seconds };
            PhotonNetwork.RaiseEvent(TIMER_TICK, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            yield return new WaitForSeconds(1);
        }
    }
    #region Network event recieving
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
        if (obj.Code == TIMER_TICK)
        {
            object[] data = (object[])obj.CustomData;
            var minutes = data[0];
            var seconds = data[1];
            timerText.text = minutes + " : " + seconds;
        }
    }
    #endregion
}
