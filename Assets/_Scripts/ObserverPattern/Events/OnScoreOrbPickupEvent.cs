using System;
using UnityEngine;

public class OnScoreOrbPickupEvent : MonoBehaviour
{
    // simple trigger
    public static event Action<int> orbPickedUp;

    // trigger this event when the object 
    // the script is attached to collides with another object
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Score")
        {
            orbPickedUp?.Invoke(100);
            Destroy(other.gameObject);
        }
    }
}
