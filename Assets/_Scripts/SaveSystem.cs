using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem   
{
    public static void SavePlayer(PlayerBehaviour player)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        Debug.Log(data);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("save completed");

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            Debug.Log("load method reached");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
            
        }
        else
        {
            Debug.Log("load not completed");
            Debug.LogError("Save file not found in" + path);
            return null;
            
        }
    }

    
}
