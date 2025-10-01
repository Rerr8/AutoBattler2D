using UnityEngine;
using UnityEngine.UI;

public enum DamageType { 
    Slash, 
    Blunt, 
    Pierce 

}

[CreateAssetMenu(fileName = "Weapon", menuName = "AutoBattler/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public int baseDamage;
    public DamageType damageType;
    public Sprite weaponIcon;
}
