using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmoteWheelController : MonoBehaviour
{
    public GameObject emoteWheel; // Assign in the inspector
    public Button[] emoteButtons; // Assign in the inspector
    private int selectedEmoteIndex = -1;

    void Start()
    {
        PositionEmoteButtons();
        emoteWheel.SetActive(false); // Start with the emote wheel hidden
    }

    void Update()
    {
        // The input handling is now moved to the Player script
        // This script now only cares about highlighting and selecting emotes when the wheel is active
        if (emoteWheel.activeSelf)
        {
            Vector2 direction = (Input.mousePosition - emoteWheel.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180; // Adjust the angle
            angle = (360 + angle) % 360; // Ensure the angle is not negative

            selectedEmoteIndex = DetermineEmoteIndex(angle);
            HighlightEmote(selectedEmoteIndex);
        }
    }

    private void PositionEmoteButtons()
    {
        float angleStep = 360.0f / emoteButtons.Length;
        float radius = 100.0f; // Adjust as needed for your UI

        for (int i = 0; i < emoteButtons.Length; i++)
        {
            float angle = i * angleStep;
            Vector3 position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            emoteButtons[i].transform.localPosition = position;
        }
    }

    private int DetermineEmoteIndex(float angle)
    {
        float anglePerSection = 360f / emoteButtons.Length;
        return (int)(angle / anglePerSection);
    }

    private void HighlightEmote(int index)
    {
        for (int i = 0; i < emoteButtons.Length; i++)
        {
            emoteButtons[i].GetComponent<Image>().color = i == index ? Color.red : Color.white;
        }
    }

    public void ExecuteEmoteAction(int index)
    {
        if (index < 0 || index >= emoteButtons.Length)
        {
            return; // No valid emote selected
        }

        // Here you would add your logic to handle the emote action
        Debug.Log("Emote selected: " + emoteButtons[index].name);
        // For example, you could call a method on the Player or an Emote Manager to play the emote
    }

    // This method is called from the Player script to toggle the visibility of the emote wheel
    public void ToggleVisibility(bool show)
    {
        emoteWheel.SetActive(show);
        if (!show)
        {
            ExecuteEmoteAction(selectedEmoteIndex); // Execute the action if the wheel is being hidden
            selectedEmoteIndex = -1; // Reset the selected index
        }
    }
}
