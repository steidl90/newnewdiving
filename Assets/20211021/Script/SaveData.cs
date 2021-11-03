using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    public static void SaveStage(int stage, int sector, int mode)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stage:";
        FileStream stream = new FileStream(path, FileMode.Create);

        StageData data = new StageData(stage, sector, mode);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StageData LoadStage()
    {
        string path = Application.persistentDataPath + "/stage:";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StageData data = formatter.Deserialize(stream) as StageData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
