using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class OnlinePlayerNameUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var view = GetComponentInParent<PhotonView>();
        GetComponent<TMP_Text>().text = view.Owner.ActorNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
