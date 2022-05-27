using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : Health
{
    public Image healthUIImage;
    private void Update()
    {
        UpdateHealthUI();
    }
    public override void TakeDamage(int amount, int damager)
    {
        base.TakeDamage(amount, damager);
        canBeDamaged = false;
        Invoke("ResetDamage", 1);
    }
    public override void TriggerDeath(int damager)
    {
        base.TriggerDeath(damager);
        RoundManager.instance.UpdateScore(damager, GetComponent<PlayerInputs>().playerNum);
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetBool("Death", true);
    }
    public override void Die()
    {
        RoundManager.instance.SpawnPlayer(GetComponent<PlayerInputs>().playerNum);
        Destroy(gameObject);
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
