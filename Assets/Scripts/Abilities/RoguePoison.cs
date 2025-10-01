using UnityEngine;

[CreateAssetMenu(fileName = "Rogue Poison", menuName = "AutoBattler/Abilities/Rogue Poison")]
public class RoguePoison : AbilitySO
{
    public override int OnBeforeAttack(Character owner, Character target, int damage)
    {
        return damage + owner.turnsTaken - 1;
    }
    
}
