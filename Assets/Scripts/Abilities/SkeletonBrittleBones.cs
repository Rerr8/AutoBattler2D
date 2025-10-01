using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton BrittleBones", menuName = "AutoBattler/Abilities/Skeleton BrittleBones")]
public class SkeletonBrittleBones : AbilitySO
{
    const int MULTIPLIER = 2;
    public override int OnBeforeDamage(Character owner, Character attacker, int damage)
    {
        if (attacker.weapon.damageType == DamageType.Blunt)
        {
            return damage * MULTIPLIER;
        }
        return damage;
    }
}
