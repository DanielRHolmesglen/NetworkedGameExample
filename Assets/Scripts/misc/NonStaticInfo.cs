using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStaticInfo : MonoBehaviour
{
    public string nonStaticName;

    // Start is called before the first frame update
    void Start()
    {
        StaticInfo.staticName = "another name I guess";
        Debug.Log(StaticInfo.staticName);
    }
}
