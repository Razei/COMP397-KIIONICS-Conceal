using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnInvisUseEvent : MonoBehaviour
{
    // simple trigger
    public static event Action invisItemUseTrigger;
    public static bool isUsed;

    public static void InvokeInvisItemUsed()
    {
        invisItemUseTrigger?.Invoke();
    }
}
