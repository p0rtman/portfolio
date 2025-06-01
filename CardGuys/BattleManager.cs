using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("UI & Spawners")]
    [Tooltip("Reference to the DeckUIManager that holds the card pool")]
    public DeckUIManager deckUIManager;
    [Tooltip("Spawner for the AI side")]
    public CardSelectorManager aiSelector;

    [Header("Tags")]
    [Tooltip("Tag to assign the player's spawned units")]
    public string playerTag = "Player";
    [Tooltip("Tag to assign the AI's spawned units")]
    public string enemyTag  = "Enemy";

    void Start()
    {
        if (deckUIManager == null || aiSelector == null)
        {
            Debug.LogError("BattleManager: Missing DeckUIManager or AI Selector in Inspector!");
            return;
        }

        // Listen for when the player has finished selecting their cards
        deckUIManager.OnSelectionComplete += OnPlayerCardChosen;
        deckUIManager.Show();
        Debug.Log("BattleManager: Waiting for player to select cards...");
    }

    void OnPlayerCardChosen()
    {
        Debug.Log("BattleManager: Player finished selection — now picking for AI...");

        // Use the same pool of cards from your DeckUIManager
        var pool = deckUIManager.availableCards;
        if (pool == null || pool.Length == 0)
        {
            Debug.LogError("BattleManager: DeckUIManager.availableCards is empty!");
            return;
        }

        int rand = Random.Range(0, pool.Length);
        var aiCard = pool[rand];
        Debug.Log($"BattleManager: AI randomly picked '{aiCard.cardName}' (index {rand})");

        aiSelector.SpawnCard(aiCard, enemyTag);
    }
}
