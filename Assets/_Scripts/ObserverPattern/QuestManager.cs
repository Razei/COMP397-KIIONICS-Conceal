using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questParent;
    public int questCounter = 0;
    // "subscribe" to relevant events
    void OnEnable()
    {
        OnItemPickupEvent.itemPickedUpWithQuantity += ItemPickupQuest;
        OnInvisUseEvent.invisItemUseTrigger += activateInvisibilityQuest;
        OnItemPickupEvent.itemPickedUpWithQuantity += KeycardPickupQuest;
        OnControlUsed.controlUsed += jumpQuest;
        OnControlUsed.controlUsed += lookQuest;
    }

    //unsubscribe
    void OnDisable()
    {
        OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupQuest;
        OnInvisUseEvent.invisItemUseTrigger -= activateInvisibilityQuest;
        OnItemPickupEvent.itemPickedUpWithQuantity -= KeycardPickupQuest;
        OnControlUsed.controlUsed -= jumpQuest;
        OnControlUsed.controlUsed -= lookQuest;
    }

    void QuestCompleted(string questName)
    {
        var questItem = GameObject.Find(questName);
        questItem?.SetActive(false);

        questCounter++;

        if (questCounter >= 5)
        {
            questParent.SetActive(false);
        }
    }

    public void jumpQuest(string type)
    {
        if (type == "Jump")
        {
            QuestCompleted("Quest1");

            // unsubscibe
            OnControlUsed.controlUsed -= jumpQuest;
        }
    }

    public void lookQuest(string type)
    {
        if (type == "Look")
        {
            QuestCompleted("Quest2");

            // unsusbscibe
            OnControlUsed.controlUsed -= lookQuest;
        }

    }

    public void ItemPickupQuest(ItemObject item, int quantity)
    {
        if (item.type == ItemType.Equipment)
        {
            QuestCompleted("Quest3");

            // unsusbscibe
            OnItemPickupEvent.itemPickedUpWithQuantity -= ItemPickupQuest;
        }
    }
    public void activateInvisibilityQuest()
    {
        QuestCompleted("Quest4");

        // unsubscibe
        OnInvisUseEvent.invisItemUseTrigger -= activateInvisibilityQuest;
    }

    public void KeycardPickupQuest(ItemObject item, int quantity)
    {
        if (item.type == ItemType.Goal)
        {
            QuestCompleted("Quest5");

            // unsubscibe
            OnItemPickupEvent.itemPickedUpWithQuantity -= KeycardPickupQuest;
        }
    }
}
