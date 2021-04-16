using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    private int pickupCount = 0;
    private int equipmentPickupCount = 0;
    private int pickupGoal = 2;
    public GameObject quest;
    public GameObject achievementDisplayPrefab;

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

    private IEnumerator ShowAchievement(string message)
    {
        var obj = Instantiate(achievementDisplayPrefab, Vector3.zero, Quaternion.identity, transform);
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = Vector3.zero;

        var messageText = obj.transform.Find("Message")?.gameObject.GetComponent<Text>();
        
        if (messageText)
        {
            messageText.text = $"Achievement unlocked: {message}";
            yield return new WaitForSeconds(5f);
        }

        Destroy(obj);
        yield break;
    }

    private void ItemPickupAchievement(ItemObject item, int quantity)
    {

        quest = GameObject.Find("Quest1");
        quest?.SetActive(false);
        pickupCount += quantity;
  
        if (pickupCount >= pickupGoal)
        {
            StartCoroutine(ShowAchievement($"Pick up {pickupGoal} items"));
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
            StartCoroutine(ShowAchievement($"Pick up {pickupGoal} equipment items"));
            OnItemPickupEvent.itemPickedUpWithQuantity -= EquipmentPickupAchievement;
        }
    }
}
