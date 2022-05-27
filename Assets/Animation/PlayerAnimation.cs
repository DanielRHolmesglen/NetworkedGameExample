using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        if (v != 0 || h != 0)
        {
            anim.SetBool("MovementInput", true);
        }
        else
        {
            anim.SetBool("MovementInput", false);
        }

        anim.SetFloat("ForwardMomentum", v);
        anim.SetFloat("SideMomentum", h);*/
    }
}
