using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerInteraction : MonoBehaviour
{
    //replace this with the name of your own movement script;
    NewCharacterMovement cm;
    public GameObject currentWeapon;
    PlayerInputs inputs;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<NewCharacterMovement>();
        inputs = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputs.fire))
        {
            Shoot();
        }

        cm.anim.SetBool("Dancing", Input.GetKey(inputs.taunt));
    }
    public void Shoot()
    {
        cm.anim.SetTrigger("Shoot");
        currentWeapon.GetComponent<IShootable>().Shoot();
    }
}
