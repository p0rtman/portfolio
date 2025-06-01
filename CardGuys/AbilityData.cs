using UnityEngine;

public enum AbilityTriggerType { OnHit, OnStart, Passive }

[CreateAssetMenu(menuName = "Abilities/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public AbilityTriggerType triggerType;
    public float cooldown;
    public GameObject visualEffectPrefab;
}
