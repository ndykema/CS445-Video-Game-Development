using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using TMPro;

public class CutsceneTrigger : MonoBehaviour
{
    

    [Header("Cinematic Settings")]
    public PlayableDirector cutsceneDirector;  // The Playable Director for cutscene
    public CinemachineVirtualCamera cutsceneCamera;  // The camera for the cutscene
    public CinemachineVirtualCamera playerCamera;    // The player camera

    private bool hasTriggered = false; // To ensure the cutscene is triggered only once

    [Header("Audio Settings")]
    public AudioSource mainMusic;
    public AudioSource cutsceneMusic;
    public float musicFadeSpeed = 1f;
    


    void Start()
    {
        // Ensure the new object is not visible at the start
       
        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnd;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger area
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            // Play the second cutscene using the PlayableDirector
            StartCutscene();
            
        }

    }

    void StartCutscene()
    {
        StartCoroutine(FadeOutAudio(mainMusic, musicFadeSpeed));
        StartCoroutine(FadeInAudio(cutsceneMusic, musicFadeSpeed));

        cutsceneCamera.Priority = 100;
        playerCamera.Priority = 0;

        // Subscribe right before playing
        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnd;
            cutsceneDirector.Play();
            StartCoroutine(PlayCutscenePrompts());
            Debug.Log("CutScene Started!");
        }


    }
    void OnCutsceneEnd(PlayableDirector director)
    {
        Debug.Log("Cutscene finished, OnCutsceneEnd is being called.");

        // Check if it's the right director that finished
        if (director == cutsceneDirector)
        {
            GameBehavior.Instance.ShowEndPrompt(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                ResetGame();
            }

            // Clean up the subscription to avoid bugs
            cutsceneDirector.stopped -= OnCutsceneEnd;

        }
        else
        {
            Debug.LogWarning("The director that stopped wasn't the expected cutscene director.");
        }

    }

    private IEnumerator FadeOutAudio(AudioSource audioSource, float speed)
    {
        if (audioSource == null) yield break;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * speed;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f; // Reset for future use
    }

    private IEnumerator FadeInAudio(AudioSource audioSource, float speed)
    {
        if (audioSource == null) yield break;

        audioSource.Play();
        audioSource.volume = 0f;

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime * speed;
            yield return null;
        }
    }


    private IEnumerator PlayCutscenePrompts()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(2f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Mortal soul...you've completed my first trial...I may have underestimated your will...");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Many have failed, but you...There is potential...");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("You didn't think this was all you needed to complete did you? There are 3 more trials...");

        yield return new WaitForSeconds(8f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Prove yourself to the end, and I shall arm you with my power. Fail...and Death awaits you.");

    }


    private void ResetGame()
    {
        // Example of restarting the scene (reset the game)
        // You can also transition to the next level if needed
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}