using System;
using UnityEngine;

public class OnGoalEvent : MonoBehaviour
{
    // trigger and return the item picked up with a quantity
    public static event Action goalEvent;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerBehaviour>();

        if(player)
        {
            bool goalObjectExists = player.inventory.container.Exists(obj => obj.item.type == ItemType.Goal);

            if (goalObjectExists)
                goalEvent?.Invoke();
        }
    }
}
