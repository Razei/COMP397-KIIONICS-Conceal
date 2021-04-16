using System;
using UnityEngine;

public class PlayerHitEvent : MonoBehaviour
{
    // trigger and return the item picked up with a quantity
    public static event Action<int> playerHitEvent;

    public static void InvokePlayerHitEvent()
    {
        playerHitEvent?.Invoke(10);
    }
}
