using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerMovement : MonoBehaviour
{
    [Tooltip("Movement Values")]
    [SerializeField] float speedMultiplier, rotationSpeed, gravityForce, jumpForce;

    //Components
    CharacterController cc;
    public Animator anim;
    public bool freezeMovement;

    Vector3 movementDirection;
    Vector3 playerVelocity;
    public bool groundedPlayer;
    public Transform target;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        //inputs = GetComponent<PlayerInputs>();
        //new
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine) return;

        groundedPlayer = cc.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            if (anim.GetBool("Jumping")) anim.SetBool("Jumping", false);
            playerVelocity.y = 0f;
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            movementDirection.Set(h, 0, v);
            cc.Move(movementDirection * speedMultiplier * Time.deltaTime);
            anim.SetBool("HasInput", true);
        }
        else
        {
            anim.SetBool("HasInput", false);
        }

        DetermineRotation();

        var animationVector = transform.InverseTransformDirection(cc.velocity);
        anim.SetFloat("ForwardMomentum", animationVector.z);
        anim.SetFloat("SideMomentum", animationVector.x);
        ProcessGravity();
    }
    public void ProcessGravity()
    {
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            anim.SetBool("Jumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
        }
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
    public void DetermineRotation()
    {
        if (target == null)
        {
            var desiredDirection = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed * Time.deltaTime);
        }
        else
        {
            var desiredDirection = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}