using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerInteractions : MonoBehaviour
{
    OnlinePlayerMovement cm;
    //[SerializeField] public GameObject currentWeapon;
    //PlayerInputs inputs;
    WeaponHandler weaponHandler;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<OnlinePlayerMovement>();
        view = GetComponent<PhotonView>();
        //inputs = GetComponent<PlayerInputs>();
        weaponHandler = GetComponent<WeaponHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.G)) weaponHandler.DropWeapon();
        cm.anim.SetBool("Dance", Input.GetKey(KeyCode.T));
        if (Input.GetKeyDown(KeyCode.E)) weaponHandler.PickClosestWeapon();

    }
    void Shoot()
    {
        cm.anim.SetTrigger("Shoot");
        if (weaponHandler.currentWeapon != null)
        {
            weaponHandler.currentWeapon.GetComponent<IShootable>().Shoot();
        }
        else
        {
            Debug.Log("oops no weapon");
        }
    }
}

