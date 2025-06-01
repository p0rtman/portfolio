using System.Collections.Generic;
using UnityEngine;

public class BattleLogger : MonoBehaviour
{
    public static BattleLogger Instance { get; private set; }
    private List<string> entries = new List<string>();

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>Append a line to the battle log.</summary>
    public void Log(string msg)
    {
        entries.Add(msg);
        Debug.Log(msg);
    }

    /// <summary>Return a copy of all log entries.</summary>
    public List<string> GetEntries() => new List<string>(entries);

    /// <summary>Clear all past entries; call at start of each new battle.</summary>
    public void Clear() => entries.Clear();
}
