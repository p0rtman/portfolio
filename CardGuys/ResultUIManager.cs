using UnityEngine;
using TMPro;

public class ResultUIManager : MonoBehaviour
{
    [Tooltip("The Panel containing the result text, hidden by default")]
    public GameObject panel;
    [Tooltip("The TextMeshProUGUI component for showing Win/Lose")]
    public TMP_Text resultText;

    /// <summary>
    /// Show the result; panel will become visible.
    /// </summary>
    public void ShowResult(bool playerWon)
    {
        panel.SetActive(true);
        resultText.text = playerWon ? "You Win!" : "You Lose!";
    }

    /// <summary>
    /// Hide the result panel.
    /// </summary>
    public void Hide()
    {
        panel.SetActive(false);
    }
}
