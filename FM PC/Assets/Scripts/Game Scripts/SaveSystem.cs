using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(Player player)
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSave.umi";
        var fs = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(player);

        formatter.Serialize(fs, saveData);
        fs.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/playerSave.umi";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var fs = new FileStream(path, FileMode.Open);

            SaveData loadData = formatter.Deserialize(fs) as SaveData;

            fs.Close();
            return loadData;
        }
        else
        {
            Debug.Log("Save File does not exist");
            return null;
        }
    }
}
