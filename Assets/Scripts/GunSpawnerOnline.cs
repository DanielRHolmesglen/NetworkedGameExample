using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GunSpawnerOnline : MonoBehaviour
{
    [Tooltip("Objects to Spawn")]
    [SerializeField] GameObject prefab;

    [Tooltip("Spawning Conditions")]
    [SerializeField] int amountsToSpawn;
    [SerializeField] int secondsBetweenSpawn;
    [SerializeField] bool activated;
    public Transform spawnPoint;


    //internal settings
    float timer;
    int amountsSpawned = 0;


    private void OnEnable()
    {
        timer = 0;
        amountsSpawned = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        timer += Time.deltaTime;
        if(activated && timer >= secondsBetweenSpawn)
        {
            AttemptToSpawn();
        }
    }
    void AttemptToSpawn()
    {
        Debug.Log("spawn Function");
        
        timer = 0;
        if(amountsSpawned < amountsToSpawn)
        {
            Debug.Log("attempt to spawn");
            PhotonNetwork.Instantiate(prefab.name, spawnPoint.position, Quaternion.identity);
            amountsSpawned++;
        }
        
    }
}
