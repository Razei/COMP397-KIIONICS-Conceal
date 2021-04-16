using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Default Object", menuName = "Inventory System/Items/Goal")]
public class GoalObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Goal;
    }
}
