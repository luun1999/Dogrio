using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEditor;

public static class SaveSystem
{
    public static void SavePlayer (GameObject playerPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + SceneManager.GetActiveScene().name + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerPosition);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player" + SceneManager.GetActiveScene().name + ".fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayerData()
    {
        File.Delete(Application.persistentDataPath + "/player" + SceneManager.GetActiveScene().name + ".fun");
    }
}
