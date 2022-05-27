using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInfo : MonoBehaviour
{
    public static string staticName;
    NonStaticInfo nonStaticInfo;

    private void Start()
    {
        nonStaticInfo = GetComponent<NonStaticInfo>();
        nonStaticInfo.nonStaticName = "a new name";
    }
}
