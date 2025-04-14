using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionCounter : MonoBehaviour
{
    private int enterPressCount = 0;
    private const int MaxPressCount = 11;

    public TextMeshProUGUI questionsAskedText;
    public GameObject inputField;
    public GameObject okButton;
    public GameObject something;
    public GameObject name;
    public GameObject chooseScreen;
    public GameObject skip;
    public GameObject questionCounter;
    public GameObject reactions;
    public GameObject emojis;

    public GameObject enableOnThirdQuestion; // Reference to the parent GameObject to be enabled
    public ParticleSystem[] particleSystems; // Reference to the ParticleSystems

    public Image sceneBackground;
    public Image suspect;

    public Animator Animations;

    void Start()
    {
        // Get all ParticleSystems on the children
        particleSystems = enableOnThirdQuestion.GetComponentsInChildren<ParticleSystem>();

        sceneBackground = enableOnThirdQuestion.transform.Find("SceneBackground").GetComponent<Image>();
        suspect = enableOnThirdQuestion.transform.Find("Suspect").GetComponent<Image>();

        Animations = GetComponent<Animator>();
    }

    void Update()
    {
        // Comment out or remove the following lines to disable the Enter key functionality
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    IncrementEnterPressCount();
        //}

        if (enterPressCount >= MaxPressCount)
        {
            if (chooseScreen != null && !chooseScreen.activeSelf)
            {
                chooseScreen.SetActive(true);

                // Play particles when max questions are reached
                PlayParticles();
            }

            if (inputField != null)
            {
                inputField.SetActive(false);
            }
            if (okButton != null)
            {
                okButton.SetActive(false);
            }
            if (something != null)
            {
                something.SetActive(false);
            }
            if (name != null)
            {
                name.SetActive(false);
            }
            if (skip != null)
            {
                skip.SetActive(false);
            }
            if (questionCounter != null)
            {
                questionCounter.SetActive(false);
            }
        }
    }

    void PlayParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            // Play the particle system
            ps.Play();
        }
    }

    public void IncrementEnterPressCount()
    {
        if (enterPressCount < MaxPressCount)
        {
            enterPressCount++;
            if (questionsAskedText != null)
            {
                questionsAskedText.text = $"{enterPressCount}/10";
            }

            // Enable parent GameObject when player asks 3 questions
            if (enterPressCount == 3 && enableOnThirdQuestion != null)
            {
                enableOnThirdQuestion.SetActive(true);

                // Play all ParticleSystems on the children
                foreach (ParticleSystem ps in particleSystems)
                {
                    ps.Play();
                }

                sceneBackground.color = HexToColor("#FFEBD8");
                suspect.color = HexToColor("#FFEBD8");
            }

            // Increase maxParticles by 2 when 5 questions are asked
            if (enterPressCount == 5)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    var mainModule = ps.main;
                    mainModule.maxParticles += 2;

                    sceneBackground.color = HexToColor("#FFDFBB");
                    suspect.color = HexToColor("#FFDFBB");
                }
            }

            if (enterPressCount == 7)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    var mainModule = ps.main;
                    mainModule.maxParticles += 5;
                    mainModule.simulationSpeed += 1;

                    sceneBackground.color = HexToColor("#FFC98B");
                    suspect.color = HexToColor("#FFC98B");
                }
            }

            if (enterPressCount == 9)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    var mainModule = ps.main;
                    mainModule.maxParticles += 100;
                    mainModule.simulationSpeed += 2;

                    sceneBackground.color = HexToColor("#FFB770");
                    suspect.color = HexToColor("#FFB770");
                }
            }

            if (enterPressCount == 11)
            {
                if (Animations != null)
                {
                    Animations.SetBool("IsLastQuestion", true);
                    Animations.SetTrigger("LastQuestion");
                }

                if (reactions != null)
                {
                    reactions.SetActive(false);
                    emojis.SetActive(false);
                }
            }
        }
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
