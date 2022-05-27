using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public GameObject lastMade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CreateObject();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(lastMade);
        }
    }
    void CreateObject()
    {
        lastMade = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
    
}
