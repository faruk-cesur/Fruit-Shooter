using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

public class SaveManager : Singleton<SaveManager>
{
    public void SaveData(Object dataObject, string jsonFileName)
    {
        string jsonDataFile = Application.persistentDataPath + "/" + jsonFileName + ".json";
        string writeDataToJson = JsonUtility.ToJson(dataObject);

        File.WriteAllText(jsonDataFile, writeDataToJson);
    }

    public void LoadData(Object dataObject, string jsonFileName)
    {
        string jsonDataFile = Application.persistentDataPath + "/" + jsonFileName + ".json";

        if (File.Exists(jsonDataFile))
        {
            string readDataFromJson = File.ReadAllText(jsonDataFile);
            JsonUtility.FromJsonOverwrite(readDataFromJson, dataObject);
        }
    }

    public static void DeleteData(string jsonFileName)
    {
        string jsonDataFile = Application.persistentDataPath + "/" + jsonFileName + ".json";

        File.Delete(jsonDataFile);
    }
}