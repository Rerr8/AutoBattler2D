using UnityEngine;

[CreateAssetMenu(fileName = "Hidden Attack", menuName = "AutoBattler/Abilities/Hidden Attack")]
public class HiddenAttack : AbilitySO
{
    public override int OnBeforeAttack(Character owner, Character target, int damage)
    {
        if (owner.agility > target.agility)
        {
            return damage + 1;
        }
        return damage;
    }
}
