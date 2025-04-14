using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace
using TMPro;

public class EvidenceCollector : MonoBehaviour
{
    private Inventory inventory;
    private Camera mainCamera;

    public TextMeshProUGUI feedbackText; // Reference to the UI Text element

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        mainCamera = Camera.main; // Cache the main camera

        if (feedbackText == null)
        {
            Debug.LogError("Feedback Text not assigned!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Evidence"))
                {
                    AddToInventory(hit.collider.gameObject);
                }
            }
        }
    }

    private void AddToInventory(GameObject evidence)
    {
        if (inventory != null)
        {
            inventory.AddItem(evidence);
            DisplayFeedback(evidence.name);
        }
        else
        {
            Debug.LogError("Inventory not found!");
        }
    }

    private void DisplayFeedback(string itemName)
    {
        feedbackText.text = $"Picked Up {itemName}";
    }
}
