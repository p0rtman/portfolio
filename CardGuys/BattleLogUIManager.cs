using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleLogUIManager : MonoBehaviour
{
    [Tooltip("Root panel that contains the ScrollView + Continue button")]
    public GameObject panelRoot;
    [Tooltip("The Content transform inside the ScrollView")]
    public RectTransform contentArea;
    [Tooltip("Prefab of a TextMeshProUGUI for each log entry")]
    public TextMeshProUGUI entryPrefab;
    [Tooltip("Continue button that hides the log and advances")]
    public Button continueButton;

    void Awake()
    {
        panelRoot.SetActive(false);
        continueButton.onClick.AddListener(HideLog);
    }

    /// <summary>Populate and show the battle log.</summary>
    public void ShowLog()
    {
        panelRoot.SetActive(true);

        // Clear old entries
        foreach (Transform child in contentArea)
            Destroy(child.gameObject);

        // Fill with new entries
        List<string> entries = BattleLogger.Instance.GetEntries();
        foreach (string line in entries)
        {
            var txt = Instantiate(entryPrefab, contentArea);
            txt.text = line;
        }
    }

    /// <summary>Hide the log panel.</summary>
    public void HideLog()
    {
        panelRoot.SetActive(false);
        // Notify RoundManager or directly call to proceed from here:
        RoundManager.Instance.OnBattleLogComplete();
    }
}
