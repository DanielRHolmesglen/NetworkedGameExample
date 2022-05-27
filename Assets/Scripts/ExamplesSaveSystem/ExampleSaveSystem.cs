using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ExampleSaveSystem : MonoBehaviour
{
    // Makes it a singleton / single instance
    static public ExampleSaveSystem instance;
    string filePath;

    private void Awake()
    {
        // Check there are no other instances of this class in the scene
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Application.persistentDataPath + "/save.data";
    }

    public void SaveGame(ExampleGameData saveData)
    {
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public ExampleGameData LoadGame()
    {
        if (File.Exists(filePath))
        {
            // File exists 
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            ExampleGameData saveData = converter.Deserialize(dataStream) as ExampleGameData;

            dataStream.Close();
            return saveData;
        }
        else
        {
            // File does not exist
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }
    }
}
