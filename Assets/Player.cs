using System.Collections.Generic;
using UnityEngine;



// Player class inheriting from Entity
public class Player : Entity
{
    public InventoryManager inventory; // Reference to the inventory manager
    public Enemy enemy; // Reference to the enemy

    [SerializeField]
    private WeaponType currentWeaponType = WeaponType.Pistol; // Default weapon type

    // Scans inventory and updates player stats based on items
    public void ScanInventoryAndUpdateStats()
    {
        foreach (var slot in inventory.inventorySlots)
        {

            foreach (Transform child in slot.transform)
            {
                InventoryItem item = child.GetComponent<InventoryItem>();
                if (item != null)
                {
                    Debug.Log("Found item: " + item.name);
                    if (item.itemref.type == ItemType.PistolAmmo)
                    {
                        Debug.Log("Pistol ammo found: " + item.itemCount);
                    }
                }
            }
        }
        if (inventory.TorsoSlot.transform.childCount != 0)
        {
            Debug.Log("Torso slot is occupied. Checking for torso item.");
            InventoryItem torsoItem = inventory.TorsoSlot.GetComponentInChildren<InventoryItem>();
            if (torsoItem != null && torsoItem.itemref.type == ItemType.Torso)
            {
                maxHealth += 20; // Increase max health by 20 for torso item
                health += 20; // Also increase current health by 20
                Debug.Log("Torso equipped. Max health increased to: " + maxHealth);
            }
        }
        Debug.Log(inventory.TorsoSlot.transform.childCount + " items in Torso slot.");
    }

    public void Start()
    {
        ScanInventoryAndUpdateStats();
    }

    public void ChosePistol()
    {
        currentWeaponType = WeaponType.Pistol;
        Debug.Log("Current weapon set to Pistol");
    }
    public void ChoseRifle()
    {
        currentWeaponType = WeaponType.Rifle;
        Debug.Log("Current weapon set to Rifle");
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log($"{entityName} has died.");
        Destroy(gameObject); // Destroy the player game object
    }

    public override void DealDamage()
    {
        if (enemy != null)
        {
            if (WeaponType.Pistol == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 5 damage with Pistol.");
                enemy.TakeDamage(5); // Deal damage to the enemy
            }
            else if (WeaponType.Rifle == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 9 damage with Rifle.");
                enemy.TakeDamage(9); // Deal damage to the enemy
            }
        }
    }

    public override void Heal()
    {
        health += recoverHealth;

        if (health > maxHealth)
        {
            health = maxHealth; // Ensure health does not exceed maxHealth
        }
        Debug.Log($"{entityName} healed for {recoverHealth}. Current health: {health}");
    }

}

