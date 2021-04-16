using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public UIController uIController;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUM_OF_COLUMNS;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void OnEnable()
    {
        inventory.inventoryUpdatedTrigger += UpdateDisplay;
    }

    void Start()
    {
        CreateDisplay();
        uIController = FindObjectOfType<UIController>();
    }

    void Update()
    {

    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            CreateInventoryItem(i);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUM_OF_COLUMNS)), Y_START + ((-Y_SPACE_BETWEEN_ITEM * (i/NUM_OF_COLUMNS))), 0f);
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.container[i])){
                if (inventory.container[i].amount <= 0)
                {
                    Destroy(itemsDisplayed[inventory.container[i]]);
                    itemsDisplayed.Remove(inventory.container[i]);
                    inventory.container.RemoveAt(i);
                } else
                {
                    itemsDisplayed[inventory.container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
                }
            } 
            else
            {
                CreateInventoryItem(i);
            }
        }
    }

    void CreateInventoryItem(int i)
    {
        var obj = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
        obj.GetComponent<Button>().onClick.AddListener(() => {
            uIController.activateInvisibility();
            inventory.RemoveItem(inventory.container[i].item);
        });
        itemsDisplayed.Add(inventory.container[i], obj);
    }
}
