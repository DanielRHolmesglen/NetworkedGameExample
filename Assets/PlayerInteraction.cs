using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    CharacterMovement cm;
    //[SerializeField] public GameObject currentWeapon;
    PlayerInputs inputs;
    WeaponHandler weaponHandler;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        inputs = GetComponent<PlayerInputs>();
        weaponHandler = GetComponent<WeaponHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputs.fire))
        {
            Shoot();
        }
        if (Input.GetKeyDown(inputs.drop)) weaponHandler.DropWeapon();
        cm.anim.SetBool("Dance", Input.GetKey(inputs.taunt));
        if(Input.GetKeyDown(inputs.interact)) weaponHandler.PickClosestWeapon();
        
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
