using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Item dropped into slot: " + gameObject.name);
            InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
            item.parentAfterDrag = transform;
        }
        
    }
}
