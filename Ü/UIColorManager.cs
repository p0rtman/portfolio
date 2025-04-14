using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorManager : MonoBehaviour
{
    public Image positiveSliderFill; // Drag the Fill Image of Slider_positive here in the Inspector
    public Image negativeSliderFill; // Drag the Fill Image of Slider_negative here in the Inspector

    private void Start()
    {
        // Set the fill colors using the values from GameMngr's GameSettings
        positiveSliderFill.color = GameMngr.Instance.gameSettings.positiveColor;
        negativeSliderFill.color = GameMngr.Instance.gameSettings.negativeColor;
    }

    // Optionally, you can add methods here to update the colors dynamically at runtime
}
