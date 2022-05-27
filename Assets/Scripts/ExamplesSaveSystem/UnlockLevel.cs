using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel : MonoBehaviour
{
    ExampleGameMaster gm;
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<ExampleGameMaster>();
        if (gm)
        {
            Debug.Log("GM found");
        }
    }

    public void UnlockTheLevel()
    {
        gm.saveData.UnlockLevle(levelName);
        Debug.Log("attempt to save new level....");
        if (gm.saveData.unlockedLevels.Contains(levelName))
        {
            Debug.Log("saved level");
        }
        else { Debug.Log("Failed to save"); }
        ExampleSaveSystem.instance.SaveGame(gm.saveData);
    }
}
