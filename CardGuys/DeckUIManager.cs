using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class DeckUIManager : MonoBehaviour
{
    [Header("Root & UI References")]
    [Tooltip("Root GameObject containing the entire deck UI")]
    public GameObject deckUIRoot;
    [Tooltip("Parent panel where card buttons will be instantiated")]
    public RectTransform handPanel;
    [Tooltip("Prefab for each card button (must have a Button component)")]
    public Button cardButtonPrefab;
    [Tooltip("Continue button, disabled until max selections reached")]
    public Button continueButton;
    [Tooltip("TextMeshProUGUI showing how many cards are selected")]
    public TMP_Text selectionIndicator;

    [Header("Game References")]
    [Tooltip("All CardData assets available for this deck")]
    public CardData[] availableCards;
    [Tooltip("The CardSelectorManager responsible for spawning units")]
    public CardSelectorManager selectorManager;
    [Tooltip("Tag to assign spawned units (e.g. \"Player\")")]
    public string teamTag = "Player";

    [Tooltip("How many cards the player may pick before continuing")]
    public int maxSelections = 3;

    /// <summary>Fired when the player has chosen maxSelections cards and hit Continue.</summary>
    public Action OnSelectionComplete;

    private readonly List<int> selectedIndices = new List<int>();
    private readonly Dictionary<int, Button> buttonMap = new Dictionary<int, Button>();

    void Start()
    {
        continueButton.onClick.AddListener(OnContinue);
        Show();
    }

    /// <summary>Shows the panel, resets state, and populates the hand.</summary>
    public void Show()
    {
        deckUIRoot.SetActive(true);
        selectedIndices.Clear();
        buttonMap.Clear();
        PopulateHand();
        UpdateIndicator();
        UpdateContinueState();
    }

    /// <summary>Hides the entire deck UI.</summary>
    public void Hide()
    {
        deckUIRoot.SetActive(false);
    }

    void PopulateHand()
    {
        foreach (Transform t in handPanel) Destroy(t.gameObject);

        for (int i = 0; i < availableCards.Length; i++)
        {
            int idx = i;
            var data = availableCards[i];
            Button btn = Instantiate(cardButtonPrefab, handPanel);

            if (data.cardArtwork != null)
                btn.image.sprite = data.cardArtwork;

            btn.onClick.AddListener(() => OnCardButtonClicked(idx));
            buttonMap[idx] = btn;

            ResetButtonVisual(btn);
        }
    }

    void OnCardButtonClicked(int idx)
    {
        if (selectedIndices.Contains(idx))
        {
            selectedIndices.Remove(idx);
            ResetButtonVisual(buttonMap[idx]);
        }
        else if (selectedIndices.Count < maxSelections)
        {
            selectedIndices.Add(idx);
            HighlightButtonVisual(buttonMap[idx]);
        }

        UpdateIndicator();
        UpdateContinueState();
    }

    void UpdateIndicator()
    {
        selectionIndicator.text = $"{selectedIndices.Count}/{maxSelections} Cards Selected";
    }

    void UpdateContinueState()
    {
        continueButton.interactable = (selectedIndices.Count == maxSelections);
    }

    void OnContinue()
    {
        foreach (int idx in selectedIndices)
            selectorManager.SpawnCard(availableCards[idx], teamTag);

        Hide();
        OnSelectionComplete?.Invoke();
    }

    void HighlightButtonVisual(Button btn)
    {
        var c = btn.colors;
        c.normalColor = new Color(0.2f, 0.8f, 0.2f);
        btn.colors = c;
    }

    void ResetButtonVisual(Button btn)
    {
        var c = btn.colors;
        c.normalColor = Color.white;
        btn.colors = c;
    }
}
