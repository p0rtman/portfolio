using UnityEngine;

[CreateAssetMenu(menuName = "Waves/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    [Tooltip("Friendly name for this wave")]
    public string waveName;

    [Tooltip("List of CardData assets to spawn this wave")]
    public CardData[] enemies;
}
