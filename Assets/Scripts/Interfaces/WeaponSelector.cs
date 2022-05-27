using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public PlayerInteraction playerInteractions;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        playerInteractions = GetComponent<PlayerInteraction>();
        //if (currentWeapon) { playerInteractions.currentWeapon = currentWeapon.GetComponent<IShootable>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
