using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; // Array of inventory slots to hold items
    public InventorySlot TorsoSlot; // Reference to the torso slot for equipping items
    public InventorySlot HelmetSlot; // Reference to the helmet slot for equipping items
    public GameObject itemPrefab; // Prefab for the inventory item


    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem ItemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (ItemSlot != null && ItemSlot.itemref == item && ItemSlot.itemref.isStackable && ItemSlot.itemCount < 30)
            {
                // If the item is stackable and already exists in the inventory, increment the count
                ItemSlot.itemCount++; // Increment the item count
                ItemSlot.UpdateItemCount(); // Update the item count display
                return; // Exit after updating the item count
            }

        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem ItemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (ItemSlot == null)
            {
                SpawnItem(item, slot); // Spawn the item in the first empty slot
                return; // Exit after adding the item
            }

        }

        
    }

    public void SpawnItem(Item item, InventorySlot slot)
    {    
        GameObject itemObject = Instantiate(itemPrefab, slot.transform); 
        InventoryItem inventoryItem = itemObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

}

