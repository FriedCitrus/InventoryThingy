using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite itemIcon; // Icon representing the item
    public ItemType type; // Type of the item (e.g., Ammo, Equips, Health)
    public ActionType actionType; // Type of action associated with the item (e.g., Use, Wear, Buy)
    public bool isStackable; // Indicates if the item can be stacked in the inventory

}
public enum ItemType
{
    Ammo,
    Equips,
    Health
}

public enum ActionType
{
    Use,
    Wear,
    Buy
}
