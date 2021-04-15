using System;
using UnityEngine;

public class OnItemPickupEvent : MonoBehaviour
{
    public static event Action<ItemObject, int> itemPickedUp;

    // trigger this event when the object 
    // the script is attached to collides with another object
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            Debug.Log($"Item {item.item} picked up");
            itemPickedUp?.Invoke(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
