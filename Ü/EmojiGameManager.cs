using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmojiGameManager : MonoBehaviour
{ 
    public int PScore;
    public int NScore;

    public Slider PSlider;
    public Slider NSlider;

    public string nextSceneName;


    SavePlayerPos playerPosData;

    private void Update()
    {
        if (PScore >= 5)
        {
            PlayerPrefs.SetInt("Outcome", 1);
            SceneManager.LoadScene(3);
        }
        else if (NScore >= 5)
        {
            PlayerPrefs.SetInt("Outcome", 2);
            SceneManager.LoadScene(3);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Positive_Emoji"))
        {
            AddPScore();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Negative_Emoji"))
        {
            AddNScore();
            Destroy(collision.gameObject);
        }
    }

    public void AddPScore()
    {
        PScore += 1;
        PSlider.value = PScore;

        if(PScore >= 5)
        {
            Debug.Log("EndGame = Positve Status");
        }
    }

    public void AddNScore()
    {
        NScore += 1;
        NSlider.value = NScore;

        if (NScore >= 5)
        {
            Debug.Log("EndGame = Negative Status");
        }
    }
}
