using System.Collections.Generic;
using UnityEngine;

// Player class inheriting from Entity
public class Enemy : Entity
{
    public Player player; // Reference to the player

    public int HealthPacks; 

    // Scans inventory and updates player stats based on items

    [SerializeField]
    private WeaponType currentWeaponType = WeaponType.Pistol; // Default weapon type

    public void Start()
    {
        HealthPacks = Random.Range(1, 5); // Randomly assign health packs between 1 and 5
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
        Destroy(gameObject); // Destroy the enemy game object
    }

    public override void DealDamage()
    {
        if (player != null)
        {
            if (WeaponType.Pistol == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 5 damage with Pistol.");
                player.TakeDamage(5); // Deal damage to the enemy
            }
            else if (WeaponType.Rifle == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 9 damage with Rifle.");
                player.TakeDamage(9); // Deal damage to the enemy
            }
        }
    }

    public override void Heal()
    {
        HealthPacks--;
        health += recoverHealth;
        if (health > maxHealth)
        {
            health = maxHealth; // Ensure health does not exceed maxHealth
        }
        Debug.Log($"{entityName} healed for {recoverHealth}. Current health: {health}");
    }
}