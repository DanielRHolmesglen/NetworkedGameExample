using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelToSelect : MonoBehaviour
{
    public string LevelName;
    public bool IsLevelLocked;
    public GameObject lockedImage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateGraphics", 1f);
    }

    public void UpdateGraphics()
    {
        lockedImage.SetActive(IsLevelLocked);
    }
    public void AttemptToLoadLevel()
    {
        if (IsLevelLocked) { Debug.Log("locked"); return; }
        SceneManager.LoadScene(LevelName);
    }
}
