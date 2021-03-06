using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class OnlineHealth : Health
{
    public Image healthUIImage;
    public PhotonView pv;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        OnlineRoundManager.instance.pv.RPC("AddPlayersToList", RpcTarget.All);
    }
    private void Update()
    {
        UpdateHealthUI();
    }
    [PunRPC]
    public override void TakeDamage(int amount, int damager)
    {
        base.TakeDamage(amount, damager);
        canBeDamaged = false;
        Invoke("ResetDamage", 1);
    }
    public override void TriggerDeath(int damager)
    {
        base.TriggerDeath(damager);
        //OnlineRoundManager.instance.pv.RPC("UpdateScore", RpcTarget.All, damager, pv.OwnerActorNr-1);
        OnlineRoundManager.instance.UpdateScore(damager, pv.ControllerActorNr);
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetBool("Death", true);
    }
    public override void Die()
    {
       // OnlineRoundManager.instance.pv.RPC("SpawnPlayer", RpcTarget.All, pv.OwnerActorNr - 1);
        OnlineRoundManager.instance.SpawnPlayer(pv.ControllerActorNr);
        //Destroy(gameObject);
    }
    public override void UpdateHealthUI()
    {
        if (!healthUIImage) return;
        float amount = ((float)currentHealth / (float)maxHealth);
        healthUIImage.fillAmount = amount;
    }
    public void ResetDamage()
    {
        canBeDamaged = true;
        Debug.Log("can be damaged again");
    }
}
