using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemySO", menuName = "AutoBattler/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int health;
    public int strength;
    public int agility;
    public int stamina;
    public WeaponSO weapon;
    public WeaponSO rewardWeapon;
    public List<AbilitySO> abilities;
    public Sprite enemySprite;

}
