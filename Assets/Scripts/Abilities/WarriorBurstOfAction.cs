using UnityEngine;

[CreateAssetMenu(fileName = "Warrior BurstOfAction", menuName = "AutoBattler/Abilities/Warrior BurstOfAction")]
public class WarriorBurstOfAction : AbilitySO
{
    const int MULTIPLIER = 2;
    public override int OnBeforeAttack(Character owner, Character target, int damage)
    {
        if (owner.turnsTaken == 1)
        {
            return damage * MULTIPLIER;
        }
        return damage;
    }
}
