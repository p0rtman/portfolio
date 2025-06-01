using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class HealthBarUI : MonoBehaviour
{
    [Tooltip("Image type=Filled; assigned in prefab")]
    public Image fillImage;
    [Tooltip("Optional TextMeshProUGUI for numeric display")]
    public TMP_Text healthText;

    private Transform camTransform;

    void Awake()
    {
        if (Camera.main != null)
            camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (camTransform != null)
            transform.forward = camTransform.forward;
    }

    /// <summary>
    /// Updates the bar fill (0–1) AND the numeric text (if assigned).
    /// </summary>
    public void SetHealth(int current, int max)
    {
        float normalized = max > 0 ? (float)current / max : 0f;
        if (fillImage != null)
            fillImage.fillAmount = Mathf.Clamp01(normalized);

        if (healthText != null)
            healthText.text = $"{current}/{max}";
    }
}
