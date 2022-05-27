using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDamage : MonoBehaviour
{
    public int damageAmount, repeatTime;
    public bool areaActivated, autoTrigger, isTriggerCollider, canAttackMultipleTargets;

    private void OnTriggerEnter(Collider other)
    {
        if (!areaActivated || !autoTrigger || !isTriggerCollider) return;
        var health = other.GetComponent<Health>();
        if (!health) return;

        health.TakeDamage(damageAmount, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!areaActivated || !autoTrigger || isTriggerCollider) return;
        var health = collision.gameObject.GetComponent<Health>();
        if (!health) return;

        health.TakeDamage(damageAmount, 0);
    }

}
