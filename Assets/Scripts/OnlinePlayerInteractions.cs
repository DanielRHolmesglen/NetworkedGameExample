using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerInteractions : MonoBehaviour
{
    OnlinePlayerMovement cm;
    //[SerializeField] public GameObject currentWeapon;
    //PlayerInputs inputs;
    WeaponHandlerOnline weaponHandler;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<OnlinePlayerMovement>();
        view = GetComponent<PhotonView>();
        //inputs = GetComponent<PlayerInputs>();
        weaponHandler = GetComponent<WeaponHandlerOnline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            view.RPC("Shoot", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.G)) weaponHandler.pv.RPC("DropWeapon", RpcTarget.All);
        cm.anim.SetBool("Dance", Input.GetKey(KeyCode.T));
        if (Input.GetKeyDown(KeyCode.E)) weaponHandler.pv.RPC("PickClosestWeapon", RpcTarget.All);

    }
    [PunRPC]
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

