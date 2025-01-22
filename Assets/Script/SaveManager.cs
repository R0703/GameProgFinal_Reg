using UnityEngine;
using System.IO;

public class SaveManager
{
    //memastikan persistance.
    private static readonly string saveFilePath = Application.persistentDataPath + "/SaveData.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true); //ubah object di kelas SaveData ke dlm bentuk JSON

        if (!string.IsNullOrEmpty(saveFilePath))
        {
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Game Saved: " + saveFilePath);
        }
        else
        {
            Debug.LogError("Save File Path is null or empty.");
        }
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            Debug.Log("Game Loaded: " + saveFilePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogWarning("No Save File Found");
            return new SaveData(); 
        }
    }

    public static void ResetSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save File Reset");
        }
        else
        {
            Debug.LogWarning("No Save File to Reset.");
        }
    }
}
