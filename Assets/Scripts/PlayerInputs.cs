using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputs : MonoBehaviour
{
    public int playerNum = 1;
    [HideInInspector]
    public KeyCode jump, fire, taunt, interact, drop;
    [HideInInspector]
    public string horizontal, vertical;
    public string playerName;

    private void Awake()
    {
        DetermineInputs();
        UpdateUI();
    }
    public void DetermineInputs()
    {
        switch (playerNum)
        {
            case 1:
                vertical = "P1Vertical";
                horizontal = "P1Horizontal";
                jump = KeyCode.F;
                interact = KeyCode.T;
                drop = KeyCode.H;
                fire = KeyCode.G;
                taunt = KeyCode.R;
                break;
            case 2:
                vertical = "P2Vertical";
                horizontal = "P2Horizontal";
                jump = KeyCode.RightAlt;
                interact = KeyCode.Comma;
                fire = KeyCode.RightControl;
                taunt = KeyCode.Period;
                break;
                
        }
    }
    public void UpdateUI()
    {
        GetComponentInChildren<TMP_Text>().text = playerName;
    }
    
}
