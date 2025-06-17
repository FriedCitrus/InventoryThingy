using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour
{
    public string entityName; // Name of the entity
    public int health=100; // Health of the entity
    public UnityEngine.UI.Image HealthBar; // Reference to the health bar UI element
    public int maxHealth=100; // Maximum health of the entity
    public int recoverHealth = 25; // Amount of health to recover
    public int HealthPacks = 0; // Number of health packs available
    public int pistolAmmo; 
    public int rifleAmmo; 
    public int armour = 0; // Armour value of the entity


    // Method to take damage
    public abstract void TakeDamage(int damage);

    public abstract void DealDamage();

    public abstract void Heal();

    // Method to handle entity death
    public abstract void Die();
}

enum WeaponType
{
    Pistol,
    Rifle
}