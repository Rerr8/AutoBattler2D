using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PlayerData
{
    public int currentLevel;
    public const int MAX_LEVEL = 3;

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

        currentLevel = 1;
        availableTalentPoints = 1;

        RecalculateMaxHealth();
        RestoreHealth();
    }

    public void LevelUp()
    {
        if (currentLevel >= MAX_LEVEL) return;

        currentLevel++;
        availableTalentPoints += 1;
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

        if (talent.characterSprite != null && this.playerSprite == null)
        {
            this.playerSprite = talent.characterSprite;
        }

        RecalculateMaxHealth();
        RestoreHealth();
    }

    public void RecalculateMaxHealth()
    {
        int healthBonus = CalculateBonusFromTalents(talent => talent.maxHealthBonus);
        maxHealth = healthBonus + this.Stamina * this.currentLevel;
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