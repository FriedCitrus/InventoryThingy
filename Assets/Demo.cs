using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryItemManager;
    public Item[] itemToAdd;

    public void PickupItem(int itemIndex)
    {
       inventoryItemManager.AddItem(itemToAdd[itemIndex]);
    }
}