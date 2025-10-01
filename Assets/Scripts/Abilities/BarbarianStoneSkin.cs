using UnityEngine;

[CreateAssetMenu(fileName = "Barbarian StoneSkin", menuName = "AutoBattler/Abilities/Barbarian StoneSkin")]
public class BarbarianStoneSkin : AbilitySO
{
    public override int OnBeforeDamage(Character owner, Character attacker, int damage)
    {
        return Mathf.Max(0, damage - owner.stamina);
    }
}
