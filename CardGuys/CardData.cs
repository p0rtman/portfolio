using UnityEngine;

[CreateAssetMenu(menuName = "CardGuy/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardArtwork;
    public GameObject cardPrefab3D;
    public int health;
    public int damage;
    public float movementSpeed;
    public float attackSpeed;
    public AbilityData[] abilities;
}
