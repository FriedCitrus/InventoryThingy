using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public enum SlotType { Generic, Torso, Helmet }
    public SlotType slotType;
    public Player player; // Reference to the player script
    public void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
    {
        player = FindObjectOfType<Player>(); // Find the player in the scene
        if (transform.childCount == 0)
        {
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (slotType == SlotType.Torso)
            {
                if (item.itemref.type != ItemType.Torso)
                {
                    Debug.Log("Only Torso items can be dropped into this slot.");
                    return;
                }

            }
            if (slotType == SlotType.Helmet)
            {
                if (item.itemref.type != ItemType.Helmet)
                {
                    Debug.Log("Only Helmet items can be dropped into this slot.");
                    return;
                }
            }
            Debug.Log("Item dropped into slot: " + gameObject.name);
            item.parentAfterDrag = transform;
            player.ScanInventoryAndUpdateStats(); // Update player stats after item drop
        }

    }
}


