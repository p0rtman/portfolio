using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager Instance { get; private set; }

    public int PositiveOutcomes { get; private set; }
    public int NegativeOutcomes { get; private set; }
    public int MaximumOutcomes { get; private set; } = 5; // Example value

    private void Awake()
    {
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

    private void Start()
    {
        LoadOutcomes();
    }

    public void AddPositiveOutcome()
    {
        PositiveOutcomes = Mathf.Clamp(PositiveOutcomes + 1, 0, MaximumOutcomes);
        SaveOutcomes();
    }

    public void AddNegativeOutcome()
    {
        NegativeOutcomes = Mathf.Clamp(NegativeOutcomes + 1, 0, MaximumOutcomes);
        SaveOutcomes();
    }

    private void LoadOutcomes()
    {
        PositiveOutcomes = PlayerPrefs.GetInt("PositiveOutcomes", 0);
        NegativeOutcomes = PlayerPrefs.GetInt("NegativeOutcomes", 0);
    }

    private void SaveOutcomes()
    {
        PlayerPrefs.SetInt("PositiveOutcomes", PositiveOutcomes);
        PlayerPrefs.SetInt("NegativeOutcomes", NegativeOutcomes);
        PlayerPrefs.Save();
    }

    // Any other game management logic you need...jadsnjk
}
