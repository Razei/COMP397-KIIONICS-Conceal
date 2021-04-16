using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private int pickupCount = 0;
    private int equipmentPickupCount = 0;
    private int pickupGoal = 2;

    // "subscribe" to relevant events
    void OnEnable()
    {
        OnItemPickupEvent.itemPickedUpWithQuantity += ItemPickupAchievement;
    }

    //unsubscribe
    void OnDisable()
    {
        OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupAchievement;
    }

    private void ShowAchievement(string message)
    {
        Debug.Log($"Achievement unlocked: {message}");
    }

    private void ItemPickupAchievement(ItemObject item, int quantity)
    {
        bool allAchieved = pickupCount == pickupGoal && equipmentPickupCount == pickupGoal;
        pickupCount += quantity;

        if (item.type == ItemType.Equipment)
        {
            equipmentPickupCount += quantity;
        }
        
        if (pickupCount >= pickupGoal)
        {
            ShowAchievement($"Pick up {pickupGoal} items");
        }

        if (equipmentPickupCount >= pickupGoal)
        {
            ShowAchievement($"Pick up {pickupGoal} equipment items");
        }

        if (allAchieved)
        {
            //unsubscribe
            OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupAchievement;
        }
    }
}
