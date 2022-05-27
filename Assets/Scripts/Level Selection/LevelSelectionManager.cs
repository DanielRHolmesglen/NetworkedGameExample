using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public GameObject[] levels;
    public ExampleGameMaster gm;
    public int currentLevel;
    public GameObject levelSelectIndicator;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<ExampleGameMaster>();
        gm.saveData = ExampleSaveSystem.instance.LoadGame();
        if (!gm.saveData.unlockedLevels.Contains("level1")){ gm.saveData.unlockedLevels.Add("level1"); }
        currentLevel = 0;
        foreach(GameObject level in levels)
        {
            var levelscript = level.GetComponent<LevelToSelect>();
            if (gm.saveData.unlockedLevels.Contains(levelscript.LevelName))
            {
                levelscript.IsLevelLocked = false;
            }
            else
            {
                levelscript.IsLevelLocked = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        levelSelectIndicator.transform.position = Vector3.Lerp(levelSelectIndicator.transform.position, levels[currentLevel].transform.position, 0.05f);

    }
    public void MoveSelection(string direction)
    {
        if (direction == "right" && currentLevel < levels.Length) currentLevel += 1;
        else if (direction == "left" && currentLevel > 0) currentLevel -= 1;
    }
    public void LoadLevel()
    {
        levels[currentLevel].GetComponent<LevelToSelect>().AttemptToLoadLevel();
    }
}
