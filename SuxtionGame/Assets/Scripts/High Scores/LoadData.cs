using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadData
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "HighScores";
        FileStream stream = new FileStream(path, FileMode.Create); // filemode.append?

        SaveData data = new SaveData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "HighScores";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File not found");
            return null;
        }
    }
}
