using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PlayerData
{
    private int baseStrength;
    private int baseAgility;
    private int baseStamina;

    public int Strength => baseStrength + CalculateBonusFromTalents(talent => talent.strengthBonus);
    public int Agility => baseAgility + CalculateBonusFromTalents(talent => talent.agilityBonus);
    public int Stamina => baseStamina + CalculateBonusFromTalents(talent => talent.staminaBonus);

    public int currentHealth { get; set; }
    public int maxHealth { get; private set; }
    public WeaponSO currentWeapon { get; set; }

    public Dictionary<TalentSO, int> unlockedTalentsLevels = new Dictionary<TalentSO, int>();
    public int availableTalentPoints = 0;

    public Sprite playerSprite;



    public PlayerData()
    {
        // Генерируем случайные базовые статы
        baseStrength = Random.Range(1, 4);
        baseAgility = Random.Range(1, 4);
        baseStamina = Random.Range(1, 4);

        currentWeapon = null;
        availableTalentPoints = 1;

        CalculateStats();
        RestoreHealth();
    }

    public void UpgradeTalent(TalentSO talent)
    {
        if (availableTalentPoints <= 0) return;

        int currentLevel = unlockedTalentsLevels.GetValueOrDefault(talent, 0);
        if (currentLevel >= talent.maxLevel) return;

        availableTalentPoints--;
        unlockedTalentsLevels[talent] = currentLevel + 1;

        if (talent.weaponGranted != null && this.currentWeapon == null)
        {
            this.currentWeapon = talent.weaponGranted;
        }

        CalculateStats();
        RestoreHealth();
    }

    public void CalculateStats()
    {
        int healthBonus = CalculateBonusFromTalents(talent => talent.maxHealthBonus);
        maxHealth = healthBonus + this.Stamina;
    }


    public List<AbilitySO> GetActiveAbilities()
    {
        var abilities = new List<AbilitySO>();
        foreach (var talentEntry in unlockedTalentsLevels)
        {
            if (talentEntry.Value > 0)
            {
                abilities.AddRange(talentEntry.Key.abilitiesGranted);
            }
        }
        return abilities;
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }
    
    private int CalculateBonusFromTalents(System.Func<TalentSO, int> statSelector)
    {
        int bonus = 0;
        foreach (var talentEntry in unlockedTalentsLevels)
        {
            TalentSO talent = talentEntry.Key;
            int level = talentEntry.Value;
            bonus += statSelector(talent) * level;
        }
        return bonus;
    }
}