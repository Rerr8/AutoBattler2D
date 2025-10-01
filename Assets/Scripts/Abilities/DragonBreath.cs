using UnityEngine;

[CreateAssetMenu(fileName = "Dragon Breath", menuName = "AutoBattler/Abilities/Dragon Breath")]
public class DragonBreath : AbilitySO
{
    public override int OnBeforeAttack(Character owner, Character attacker, int damage)
    {
        if (owner.turnsTaken % 3 == 0)
        {
            return damage + 3;
        }
        
        return damage;
    }
}
