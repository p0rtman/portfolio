using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterrogationSceneManager : MonoBehaviour
{
    public TextMeshProUGUI fileText; // Assign this in the inspector

    void Start()
    {
        Inventory.Instance?.SetFileTextReference(fileText);
    }

    // Call this method when the file is opened to update the list
    public void OnFileOpened()
    {
        Inventory.Instance?.UpdateFileText();
    }
}
