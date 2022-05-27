using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour, IShootable, IReloadable
{
    public void Shoot()
    {
        Debug.Log("Shwing crossbow shot");
    }
    public void Reload()
    {

    }
}
