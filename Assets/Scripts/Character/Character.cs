using UnityEngine;
using System.Collections.Generic;
public class Character
{
    public string name;
    public int level = 1;
    public int strength;
    public int agility;
    public int stamina;
    public int currentHealth;
    public int maxHealth;
    public WeaponSO weapon;
    public WeaponSO rewardWeapon;
    public int turnsTaken = 0;
    public List<AbilitySO> abilities = new();
    //public List<EffectData> activeEffects = new();
    public Sprite characterSprite;

    public Character(string name, int str, int agi, int stam, int baseHealth, WeaponSO w, WeaponSO rewardW = null, int lvl = 1)
    {
        this.name = name;
        level = lvl;
        strength = str;
        agility = agi;
        stamina = stam;
        maxHealth = baseHealth + stamina;
        currentHealth = maxHealth;
        weapon = w;
        rewardWeapon = rewardW;
        rewardWeapon = null;
    }

    public Character(PlayerData player)
    {
        name = "Player";
        level = player.currentLevel;
        strength = player.Strength;
        agility = player.Agility;
        stamina = player.Stamina;
        maxHealth = player.maxHealth;
        currentHealth = player.currentHealth;
        weapon = player.currentWeapon;
        abilities = player.GetActiveAbilities();
        characterSprite = player.playerSprite;
        
    }

    public Character(EnemySO enemy)
    {
        name = enemy.name;
        strength = enemy.strength;
        agility = enemy.agility;
        stamina = enemy.stamina;
        maxHealth = enemy.health + stamina;
        currentHealth = maxHealth;
        weapon = enemy.weapon;
        rewardWeapon = enemy.rewardWeapon;
        abilities = enemy.abilities;
        characterSprite = enemy.enemySprite;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth = Mathf.Max(0, currentHealth - dmg);
    }

    public bool IsAlive => currentHealth > 0;
}
