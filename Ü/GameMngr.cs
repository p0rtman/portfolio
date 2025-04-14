using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMngr : MonoBehaviour
{
    public static GameMngr Instance { get; private set; }

    public GameSettings gameSettings;

    private void Awake()
    {
        // Ensure there's only one GameMngr instance (singleton pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Other methods to distribute settings to various managers can go here
}
