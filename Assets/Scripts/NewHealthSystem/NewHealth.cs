using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHealth : MonoBehaviour
{
    [Tooltip("Health Amounts")]
    public int maxHealth, startingHealth, currentHealth;
    public bool canBeDamaged;

    //components
    public Slider healthUI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthUI();
    }

    public virtual void TakeDamage(int amount)
    {
        if (!canBeDamaged)
        {
            Debug.Log("Attempted to damage " + gameObject.name + " but object can not be damaged right now");
            return;
        }
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            TriggerDeath();
        }
        UpdateHealthUI();
    }
    public virtual void TriggerDeath()
    {
        //animate death, player sound and particles etc
        Debug.Log(gameObject.name + " has been killed");
        canBeDamaged = false;
        Invoke("Die", 2f);
    }
    public virtual void Die()
    {
        Debug.Log(gameObject.name + " removed from scene");
        Destroy(gameObject);
    }
    private void UpdateHealthUI()
    {
        if (!healthUI) return;
        healthUI.maxValue = maxHealth;
        healthUI.value = currentHealth;
    }
}
