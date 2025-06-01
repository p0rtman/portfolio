using UnityEngine;
using UnityEngine.UI;
using System;

public class WavePreviewUI : MonoBehaviour
{
    [Tooltip("Container for enemy icons")]
    public RectTransform content;
    [Tooltip("Prefab: simple UI Image for each icon")]
    public Image iconPrefab;
    [Tooltip("Continue button - will hide this panel")]
    public Button continueButton;

    public Action OnPreviewComplete;

    void Awake()
    {
        gameObject.SetActive(false);
        continueButton.onClick.AddListener(() =>
        {
            Hide();
            OnPreviewComplete?.Invoke();
        });
    }

    /// <summary>Show the wave preview panel, populate icons.</summary>
    public void Show(EnemyWaveData wave)
    {
        gameObject.SetActive(true);
        foreach (Transform t in content) Destroy(t.gameObject);

        foreach (var card in wave.enemies)
        {
            var img = Instantiate(iconPrefab, content);
            img.sprite = card.cardArtwork;
        }
    }

    /// <summary>Hide the wave preview panel.</summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
