using System;
using UnityEngine;

public enum DataOperation
{
    SAVE,
    LOAD
}

public class LoadSaveEvent : MonoBehaviour
{
    // trigger and return the item picked up with a quantity
    public static event Action<DataOperation> dataOperationEvent;

    public static void SaveData()
    {
        dataOperationEvent.Invoke(DataOperation.SAVE);
    }

    public static void LoadData()
    {
        dataOperationEvent.Invoke(DataOperation.LOAD);
    }
}
