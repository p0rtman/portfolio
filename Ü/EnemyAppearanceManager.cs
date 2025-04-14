using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAppearanceManager : MonoBehaviour
{
    public Image enemyImage; // Reference to the Image component of your enemy

    private void Start()
    {
        // Access the enemySprite from the GameMngr's GameSettings
        enemyImage.sprite = GameMngr.Instance.gameSettings.enemySprite;
    }

    // Optionally, you can add methods here to update the sprite dynamically at runtime
}
