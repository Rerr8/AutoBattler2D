using UnityEngine;

[CreateAssetMenu(fileName = "Warrior Shield", menuName = "AutoBattler/Abilities/Warrior Shield")]
public class WarriorShield : AbilitySO
{
    public override int OnBeforeDamage(Character owner, Character attacker, int damage)
    {
        if (owner.strength > attacker.strength)
        {
            return Mathf.Max(0, damage - 3);
        }

        return damage;
    }
}
