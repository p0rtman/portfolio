using UnityEngine;
using System.Collections;
using System.Linq;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("UI Managers")]
    public WavePreviewUI      wavePreviewUI;
    public DeckUIManager      deckUI;
    public BattleLogUIManager logUI;
    public ResultUIManager    resultUI;
    public UpgradeUIManager   upgradeUI;

    [Header("Spawners & Waves")]
    public CardSelectorManager playerSelector;  // if used
    public CardSelectorManager aiSelector;
    public EnemyWaveData[]     waves;

    [Header("Tags")]
    public string playerTag = "Player";
    public string enemyTag  = "Enemy";

    enum Phase { WavePreview, Deck, Battle, Log, Upgrade }
    Phase phase;

    int currentWave = 0;
    bool lastBattlePlayerWon;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Hook UI callbacks
        wavePreviewUI.OnPreviewComplete += OnWavePreviewComplete;
        deckUI.OnSelectionComplete   += OnPlayerPicked;

        // Prepare log UI
        logUI.panelRoot.SetActive(false);
        logUI.continueButton.onClick.AddListener(OnBattleLogComplete);

        // Prepare result & upgrade UI
        resultUI.Hide();
        upgradeUI.OnUpgradeChosen += OnUpgradePicked;

        EnterWavePreview();
    }

    void EnterWavePreview()
    {
        phase = Phase.WavePreview;
        if (currentWave < waves.Length)
            wavePreviewUI.Show(waves[currentWave]);
        else
            Debug.Log("All waves complete!");
    }

    void OnWavePreviewComplete()
    {
        phase = Phase.Deck;
        deckUI.Show();
    }

    void OnPlayerPicked()
    {
        deckUI.Hide();
        wavePreviewUI.Hide();

        // Spawn all enemies for this wave
        foreach (var e in waves[currentWave].enemies)
            aiSelector.SpawnCard(e, enemyTag);

        // Clear and start logging
        BattleLogger.Instance.Clear();

        phase = Phase.Battle;
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        // Wait until one side is wiped
        while (true)
        {
            var all = FindObjectsOfType<CardGuyController>();
            bool anyPlayer = all.Any(c => c.gameObject.tag == playerTag);
            bool anyEnemy  = all.Any(c => c.gameObject.tag == enemyTag);
            if (!anyPlayer || !anyEnemy)
            {
                lastBattlePlayerWon = anyPlayer && !anyEnemy;
                break;
            }
            yield return null;
        }

        // Show scrollable log
        phase = Phase.Log;
        logUI.ShowLog();
    }

    /// <summary>Public so BattleLogUIManager can call it.</summary>
    public void OnBattleLogComplete()
    {
        phase = Phase.Upgrade;
        resultUI.ShowResult(lastBattlePlayerWon);
        upgradeUI.Show();
    }

    void OnUpgradePicked(UpgradeData upg)
    {
        resultUI.Hide();

        var survivor = FindObjectsOfType<CardGuyController>()
            .FirstOrDefault(c => c.gameObject.tag == playerTag);
        if (survivor != null)
            survivor.ApplyUpgrade(upg);

        // Clean up and go to next wave
        CleanupField();
        currentWave++;
        EnterWavePreview();
    }

    void CleanupField()
    {
        foreach (var u in FindObjectsOfType<CardGuyController>())
            Destroy(u.gameObject);
    }
}
