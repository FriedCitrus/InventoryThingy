using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Inventory Item Properties")]
    public UnityEngine.UI.Image image; // Reference to the Image component for displaying the item
    public TextMeshProUGUI countText; // Reference to the Text component for displaying item count (if stackable)

    public Item itemref; // Reference to the Item scriptable object containing item data
    
    public Player player; // Reference to the Player script to access player-related functionality

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int itemCount = 1;

    public void InitializeItem(Item newItem)
    {
        itemref = newItem; // Assign the new item to the inventory item
        image.sprite = itemref.itemIcon; // Set the item's icon in the Image component
        image.preserveAspect = true; // Ensure the aspect ratio of the icon is preserved
        itemCount = Random.Range(1, 10); // Randomly set the item count between 1 and 10 for demonstration purposes
        UpdateItemCount(); // Update the item count text if applicable
    }

    public void UpdateItemCount()
    {
        countText.text = itemCount.ToString(); // Update the count text to reflect the new item count
        bool textActive = itemref.isStackable && itemCount > 1; // Show count text only if the item is stackable and count is greater than 1
        countText.gameObject.SetActive(textActive); // Activate or deactivate the count text based on the conditions
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Code to handle the beginning of a drag operation
        Debug.Log("Drag started on: " + gameObject.name);
        image.raycastTarget = false; // Disable raycast target to allow interaction with the item during drag
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Move the item to the root to avoid hierarchy issues during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Code to handle the dragging of the item
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Code to handle the end of a drag operation
        image.raycastTarget = true; // Re-enable raycast target to allow interaction with the item
        Debug.Log("Drag ended on: " + gameObject.name);
        transform.SetParent(parentAfterDrag); // Return the item to its original parent
        player.ScanInventoryAndUpdateStats(); // Update player stats after the drag operation
        
    }
}

