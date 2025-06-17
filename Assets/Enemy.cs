using System.Collections.Generic;
using UnityEngine;

// Player class inheriting from Entity
public class Enemy : Entity
{
    public Player player; // Reference to the player

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
        HealthBar.fillAmount = Mathf.Clamp01(health / (float)maxHealth); // Update health bar fill amount
        if (health <= 0)
        {
            Die();
        }
        DealDamage(); // Deal damage to the player after taking damage
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
            // Randomly choose between Pistol and Rifle
            currentWeaponType = (Random.value < 0.5f) ? WeaponType.Pistol : WeaponType.Rifle;

            if (WeaponType.Pistol == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 5 damage with Pistol.");
                player.TakeDamage(5);
            }
            else if (WeaponType.Rifle == currentWeaponType)
            {
                Debug.Log($"{entityName} deals 9 damage with Rifle.");
                player.TakeDamage(9);
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