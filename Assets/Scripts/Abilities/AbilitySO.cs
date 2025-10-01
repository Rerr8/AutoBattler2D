using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public string abilityName;
    [TextArea]
    public string description;

    public virtual int OnBeforeAttack(Character owner, Character target, int damage) { return damage; }
    public virtual int OnBeforeDamage(Character owner, Character attacker, int damage) { return damage; }
}
