using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] playerScoreUIs;
    [SerializeField] CanvasGroup WinScreen;
    [SerializeField] TMP_Text winningPlayerName;
    [SerializeField] TMP_Text Timer;

    public void UpdateScoreUI()
    {
       for(int i = 0; i < playerScoreUIs.Length; i++)
        {
            playerScoreUIs[i].text = "Player " + (i + 1).ToString() + " : " + RoundManager.instance.playerScores[i].ToString(); 
        }
    }
    public void DisplayResults(int winningPlayer)
    {
        winningPlayerName.text = "Player " + (winningPlayer).ToString();
        WinScreen.gameObject.SetActive(true);
        StartCoroutine(CanvasFadeIn());
    }
    public void DisplayTimer(int time)
    {
        var minutes = Mathf.Floor(time / 60).ToString("00");
        var seconds = (time % 60).ToString("00");

        Timer.text = minutes + ":" + seconds;

    }
    IEnumerator CanvasFadeIn()
    {
        WaitForEndOfFrame WFEOF = new WaitForEndOfFrame();
        while(WinScreen.alpha < 0.99)
        {
            WinScreen.alpha = Mathf.Lerp(WinScreen.alpha, 1, 0.01f);
            yield return WFEOF;
        }
        yield return null;
    }
}
