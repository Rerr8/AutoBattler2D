using UnityEngine;

[CreateAssetMenu(fileName = "Slime AmorphousBody", menuName = "AutoBattler/Abilities/Slime AmorphousBody")]
public class SlimeAmorphousBody : AbilitySO
{
    public override int OnBeforeDamage(Character owner, Character attacker, int damage)
    {
        if (attacker.weapon.damageType == DamageType.Slash)
        {
            int weaponBaseDamage = attacker.weapon.baseDamage;
            return Mathf.Max(0, damage - weaponBaseDamage);
        }

        return damage;
    }
}
