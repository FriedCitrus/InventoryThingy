using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryItemManager;
    public Item[] itemToAdd;
    
    Player player;
    

    public void PickupItem(int itemIndex)
    {
        for (int i = 0; i < 20; i++)
        {
            inventoryItemManager.AddItem(itemToAdd[itemIndex]);
        }


    }
}