using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Achievement : MySingleton<Achievement>
{
    private bool[] unlock = new bool[2] { false, false };

    public void setUnlock(int pos, bool b)
    {
        unlock[pos] = b;
    }

    public bool getUnlock(int pos)
    {
        return unlock[pos];
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/achievement.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, unlock);
        stream.Close();
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/achievement.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            unlock = formatter.Deserialize(stream) as bool[];
        }
        else
        {
            Debug.Log("File not Exist");
        }
    }
}
