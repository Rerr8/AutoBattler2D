using UnityEngine;

[CreateAssetMenu(fileName = "Barbarian Rage", menuName = "AutoBattler/Abilities/Barbarian Rage")]
public class BarbarianRage : AbilitySO
{
    public override int OnBeforeAttack(Character owner, Character target, int damage)
    {
        if (owner.turnsTaken < 3)
        {
            return damage + 2;
        }
        else
        {
            return damage - 1;
        }
    }
}
