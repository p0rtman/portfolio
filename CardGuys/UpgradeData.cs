using UnityEngine;

public enum CardStat
{
    Health,
    Damage,
    Speed,
    AttackSpeed
}

public enum UpgradeType
{
    StatBoost,
    NewAbility
}

[CreateAssetMenu(menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [Header("Basic Info")]
    public string upgradeName;
    [TextArea] public string description;
    public UpgradeType type;

    [Header("Stat Boost Settings")]
    public CardStat statToBoost;
    public float boostAmount;

    [Header("New Ability Settings")]
    public AbilityData abilityToAdd;
}
