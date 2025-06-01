using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeUIManager : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform panel;
    public Button upgradeButtonPrefab;
    public UpgradeData[] availableUpgrades;
    public Action<UpgradeData> OnUpgradeChosen;

    void Awake()
    {
        // Hide just the panel itself
        if (panel != null)
            panel.gameObject.SetActive(false);
    }

    public void Show()
    {
        if (panel == null || upgradeButtonPrefab == null || availableUpgrades == null)
        {
            Debug.LogError("UpgradeUIManager: Missing Inspector assignments!");
            return;
        }

        panel.gameObject.SetActive(true);
        PopulateUpgrades();
    }

    void PopulateUpgrades()
    {
        foreach (Transform c in panel) Destroy(c.gameObject);

        for (int i = 0; i < availableUpgrades.Length; i++)
        {
            var upg = availableUpgrades[i];
            if (upg == null) continue;
            int idx = i;

            var btn = Instantiate(upgradeButtonPrefab, panel);

            // Text or TMP_Text
            if (btn.GetComponentInChildren<Text>() is Text t) t.text = upg.upgradeName;
            else if (btn.GetComponentInChildren<TMP_Text>() is TMP_Text tmp) tmp.text = upg.upgradeName;

            btn.onClick.AddListener(() =>
            {
                OnUpgradeChosen?.Invoke(upg);
                // ** Hide only the upgrade panel, not the Canvas **
                panel.gameObject.SetActive(false);
            });
        }
    }
}
