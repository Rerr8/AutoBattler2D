using UnityEngine;

[CreateAssetMenu(fileName = "Warrior BurstOfAction", menuName = "AutoBattler/Abilities/Warrior BurstOfAction")]
public class WarriorBurstOfAction : AbilitySO
{
    public override int OnBeforeAttack(Character owner, Character target, int damage)
    {
        if (owner.turnsTaken == 0)
        {
            int weaponDamage = owner.weapon.baseDamage;
            return damage + weaponDamage;
        }
        return damage;
    }
}
