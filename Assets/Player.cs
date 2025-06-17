using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;



// Player class inheriting from Entity
public class Player : Entity
{
    public InventoryManager inventory; // Reference to the inventory manager
    public Enemy enemy; // Reference to the enemy

    [SerializeField]
    private WeaponType currentWeaponType = WeaponType.Pistol; // Default weapon type

#region Inventory Behaviour

    // Scans inventory and updates player stats based on items
    public void ScanInventoryAndUpdateStats()
    {
        InvCheck(); // Check inventory items before equipping
        EquipCheck(); // Check equipped items after scanning inventory
    }

    public void EquipCheck()
    {
        armour = 0; // Reset armour value before checking equipped items
        foreach (var slot in inventory.EquipSlots)
        {
            if (slot.transform.childCount == 0)
            {
                Debug.Log("No items equipped in this slot.");
                continue; // Skip to the next slot if no item is equipped
            }
            InventoryItem equipItem = slot.GetComponentInChildren<InventoryItem>();
            if (equipItem != null)
            {
                Debug.Log("Equipped item: " + equipItem.name);
                armour += equipItem.itemref.Armour; // Update armour value from equipped item
            }
        }
    }
    public void InvCheck()
    {
        foreach (var slot in inventory.inventorySlots)
        {

            foreach (Transform child in slot.transform)
            {
                InventoryItem item = child.GetComponent<InventoryItem>();
                if (item != null)
                {
                    Debug.Log("Found item: " + item.name);
                    switch (item.itemref.type)
                    {
                        case ItemType.PistolAmmo:
                            pistolAmmo = item.itemCount; // Add pistol ammo count
                            Debug.Log($"Pistol ammo updated: {pistolAmmo}");
                            break;
                        case ItemType.RifleAmmo:
                            rifleAmmo = item.itemCount; // Add rifle ammo count
                            Debug.Log($"Rifle ammo updated: {rifleAmmo}");
                            break;
                        case ItemType.Health:
                            HealthPacks = item.itemCount; // Add health packs count
                            Debug.Log("Health packs updated: " + HealthPacks);
                            break;
                    }

                }
            }
        }
    }

#endregion

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
        health -= (int)(damage*(1 - (armour / 100f))); // Apply armour reduction to damage
        HealthBar.fillAmount = Mathf.Clamp01(health / (float)maxHealth); // Update health bar fill amount
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
                if (pistolAmmo <= 0)
                {
                    Debug.Log($"{entityName} has no Pistol Ammo left!");
                    return; // Exit if no ammo is available
                }
                pistolAmmo--; // Decrease ammo count
                Debug.Log($"{entityName} deals 5 damage with Pistol.");
                enemy.TakeDamage(5); // Deal damage to the enemy
            }
            else if (WeaponType.Rifle == currentWeaponType)
            {
                if (rifleAmmo <= 0)
                {
                    Debug.Log($"{entityName} has no Rifle Ammo left!");
                    return; // Exit if no ammo is available
                }
                rifleAmmo--; // Decrease ammo count
                Debug.Log($"{entityName} deals 9 damage with Rifle.");
                enemy.TakeDamage(9); // Deal damage to the enemy
            }
        }
    }

    public override void Heal()
    {
        if (HealthPacks <= 0)
        {
            Debug.Log($"{entityName} has no Health Packs left!");
            return; // Exit if no health packs are available
        }
        HealthPacks--; // Decrease health packs count
        health += recoverHealth;

        if (health > maxHealth)
        {
            health = maxHealth; // Ensure health does not exceed maxHealth
        }
        Debug.Log($"{entityName} healed for {recoverHealth}. Current health: {health}");
        HealthBar.fillAmount = Mathf.Clamp01(health / (float)maxHealth); // Update health bar fill amount
    }

}

