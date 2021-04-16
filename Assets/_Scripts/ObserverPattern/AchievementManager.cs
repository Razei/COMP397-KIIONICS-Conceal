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
        OnItemPickupEvent.itemPickedUpWithQuantity += EquipmentPickupAchievement;
    }

    //unsubscribe
    void OnDisable()
    {
        OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupAchievement;
        OnItemPickupEvent.itemPickedUpWithQuantity -= EquipmentPickupAchievement;
    }

    private void ShowAchievement(string message)
    {
        Debug.Log($"Achievement unlocked: {message}");
    }

    private void ItemPickupAchievement(ItemObject item, int quantity)
    {
        pickupCount += quantity;
  
        if (pickupCount >= pickupGoal)
        {
            ShowAchievement($"Pick up {pickupGoal} items");
            OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupAchievement;
        }
    }

    private void EquipmentPickupAchievement(ItemObject item, int quantity)
    {
        if (item.type == ItemType.Equipment)
        {
            equipmentPickupCount += quantity;
        }

        if (equipmentPickupCount >= pickupGoal)
        {
            ShowAchievement($"Pick up {pickupGoal} equipment items");
            OnItemPickupEvent.itemPickedUpWithQuantity -= EquipmentPickupAchievement;
        }
    }
}
