using System;
using UnityEngine;

public class OnItemPickupEvent : MonoBehaviour
{
    // simple trigger
    public static event Action itemPickedUpTrigger;

    // trigger and return the item picked up with a quantity
    public static event Action<ItemObject, int> itemPickedUpWithQuantity;

    // trigger this event when the object 
    // the script is attached to collides with another object
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            Debug.Log($"Item {item.item} picked up");
            itemPickedUpWithQuantity?.Invoke(item.item, 1);
            itemPickedUpTrigger?.Invoke();
            Destroy(other.gameObject);
        }
    }
}
