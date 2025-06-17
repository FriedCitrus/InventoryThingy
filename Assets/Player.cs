using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;



// Player class inheriting from Entity
public class Player : Entity
{
    public InventoryManager inventory; // Reference to the inventory manager
    public Enemy enemy; // Reference to the enemy

    [HideInInspector]public InventoryItem PistolAmmoItem; // Reference to the pistol ammo item
    [HideInInspector]public InventoryItem RifleAmmoItem; // Reference to the rifle ammo item
    [HideInInspector]public InventoryItem HealthItem; // Reference to the health item
    public GameObject DeathScreen; // Reference to the win screen UI

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
                            PistolAmmoItem = item; // Store reference to the pistol ammo item
                            Debug.Log($"Pistol ammo updated: {pistolAmmo}");
                            break;
                        case ItemType.RifleAmmo:
                            rifleAmmo = item.itemCount; // Add rifle ammo count
                            RifleAmmoItem = item; // Store reference to the rifle ammo item
                            Debug.Log($"Rifle ammo updated: {rifleAmmo}");
                            break;
                        case ItemType.Health:
                            HealthPacks = item.itemCount; // Add health packs count
                            HealthItem = item; // Store reference to the health item
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
        ScanInventoryAndUpdateStats();
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log($"{entityName} has died.");
        DeathScreen.SetActive(true); // Show the death screen UI
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
                inventory.RemoveItem(PistolAmmoItem); // Remove one ammo from the inventory
                Debug.Log($"{entityName} deals 5 damage with Pistol.");
                ScanInventoryAndUpdateStats();
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
                inventory.RemoveItem(RifleAmmoItem); // Remove one ammo from the inventory
                Debug.Log($"{entityName} deals 9 damage with Rifle.");
                ScanInventoryAndUpdateStats();
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
        inventory.RemoveItem(HealthItem); // Remove one health pack from the inventory
        ScanInventoryAndUpdateStats();
    }

}

