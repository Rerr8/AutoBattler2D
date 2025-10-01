using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TalentSO", menuName = "AutoBattler/TalentSO")]
public class TalentSO : ScriptableObject
{
    public string talentName;
    public string description;
    public int maxLevel;
    public Sprite talentIcon;
    public int maxHealthBonus;
    public int strengthBonus;
    public int agilityBonus;
    public int staminaBonus;
    public List<AbilitySO> abilitiesGranted;
    public WeaponSO weaponGranted;
    public Sprite characterSprite;

}


