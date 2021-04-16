using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    // simple trigger
    public event Action inventoryUpdatedTrigger;

    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int  i= 0; i < container.Count; i++)
        {
            if (container[i].item == _item){
                container[i].AddAmount(_amount);
                inventoryUpdatedTrigger?.Invoke();
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            container.Add(new InventorySlot(_item, _amount));
            inventoryUpdatedTrigger?.Invoke();
        }
    }

    public void RemoveItem(ItemObject _item)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(-1);
                inventoryUpdatedTrigger?.Invoke();
                break;
            }
        }
    }

    public void ClearInventory()
    {
        container.Clear();
        inventoryUpdatedTrigger?.Invoke();
    }

    public void InventoryUpdatedEventTest()
    {
        Debug.Log("Inventory updated");
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}