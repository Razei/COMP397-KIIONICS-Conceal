using System;
using UnityEngine;

public class OnControlUsed : MonoBehaviour
{
    // simple trigger
    public static event Action<string> controlUsed;
    public static void InvokeControlPressed(string type)
    {
        controlUsed?.Invoke(type);
    }
}
