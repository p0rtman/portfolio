using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    // Changed to store item names instead of GameObjects
    public List<string> itemNames = new List<string>();
    private TextMeshProUGUI fileText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Method to add item names to the list
    public void AddItem(GameObject item)
    {
        if (item != null)
        {
            itemNames.Add(item.name); // Add the item's name to the list
            item.SetActive(false);
            Debug.Log("Item added to inventory: " + item.name);
        }
    }

    // Method to assign and update the fileText in the UI
    public void SetFileTextReference(TextMeshProUGUI textReference)
    {
        fileText = textReference;
        UpdateFileText(); // Update the file text whenever a new reference is set
    }

    // Method to update the fileText UI element with the list of item names
    public void UpdateFileText()
    {
        if (fileText != null)
        {
            fileText.text = ""; // Clear existing text
            foreach (string itemName in itemNames) // Iterate through stored item names
            {
                fileText.text += itemName + "\n"; // Add each item name to the text
            }
        }
    }
}
