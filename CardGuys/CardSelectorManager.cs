using UnityEngine;

public class CardSelectorManager : MonoBehaviour
{
    [Tooltip("Where to spawn the CardGuy 3D model")]
    public Transform spawnPoint;

    [Tooltip("World-space health-bar prefab")]
    public GameObject healthBarPrefab;

    /// <summary>
    /// Spawns a Card Guy based on the given CardData and assigns it to the given team tag.
    /// </summary>
    public void SpawnCard(CardData data, string teamTag)
    {
        if (data == null)
        {
            Debug.LogError("CardSelectorManager: SpawnCard called with null CardData");
            return;
        }

        // 1) Instantiate the 3D model
        GameObject go = Instantiate(data.cardPrefab3D, spawnPoint.position, Quaternion.identity);

        // 2) Tag it for team logic
        go.tag = teamTag;

        // 3) Attach controller, initialize stats, and give it the single health-bar prefab
        var ctrl = go.AddComponent<CardGuyController>();
        ctrl.healthBarPrefab = healthBarPrefab;
        ctrl.Initialize(data);

        // 4) Attach AI so it can move and fight
        go.AddComponent<CardGuyAI>();
    }
}
