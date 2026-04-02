using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

// This script acts as a central game manager (singleton) for handling UI, torch state,
// checkpoints, cutscenes, and restarting logic.
public class GameBehavior : MonoBehaviour
{
    // Static instance for global access (Singleton pattern)
    public static GameBehavior Instance;

    // UI elements for displaying progress, prompts, and cutscenes
    public TMP_Text ProgressText;
    public TMP_Text TorchPrompt;
    public TMP_Text AmarealCutScene;

    // End game UI panel
    public GameObject ENDUIPANEL;

    // Controls whether the player is allowed to restart
    private bool canRestart = false;

    // Stores checkpoint and challenge reset positions
    public Vector3 respawnPosition;
    public Vector3 challengePosition;
    public Transform challenge;

    // Tracks number of torches the player has
    public int TorchCount { get; private set; } = 0;

    // Returns true if player has at least one torch
    public bool HasTorch => TorchCount > 0;

    // Tracks whether the torch is currently lit
    public bool IsTorchLit { get; private set; } = false;

    private void Start()
    {
        // Hide UI elements at the start of the game
        ProgressText.gameObject.SetActive(false);
        AmarealCutScene.gameObject.SetActive(false);
        ENDUIPANEL.SetActive(false);
    }

    // Updates the progress text UI with a message
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        ProgressText.gameObject.SetActive(true);

        // Hide the progress text after a short delay
        StartCoroutine(HideProgressText());
    }

    // Coroutine to hide progress text after 2 seconds
    private IEnumerator HideProgressText()
    {
        yield return new WaitForSeconds(2f);
        ProgressText.gameObject.SetActive(false);
    }

    // Adds a torch to the player's inventory
    public void AddTorch(int amount = 1)
    {
        TorchCount += amount;
        Debug.Log($"Player now has {TorchCount} torch(es)");

        // Automatically set torch as lit when picked up
        IsTorchLit = true;

        // Display UI message
        UpdateScene("You picked up a torch!");
    }

    // Removes a torch from the player's inventory
    public void RemoveTorch(int amount = 1)
    {
        TorchCount = Mathf.Max(0, TorchCount - amount);
        Debug.Log($"Player now has {TorchCount} torch(es)");

        // If no torches remain, mark torch as unlit
        if (TorchCount == 0)
        {
            IsTorchLit = false;
        }
    }

    private void Awake()
    {
        // Ensure only one instance of GameBehavior exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Shows or hides the torch pickup prompt
    public void ShowTorchPrompt(bool show)
    {
        if (TorchPrompt != null)
        {
            TorchPrompt.gameObject.SetActive(show);

            if (show)
            {
                TorchPrompt.text = "Press E to pick up the torch!";
            }
        }
    }

    // UI element for candle interaction prompt
    public TMP_Text LightCandlePrompt;

    // Tracks the current candle the player is near
    private GameObject currentCandle = null;

    // Shows or hides the candle interaction prompt
    public void ShowCandlePrompt(bool show)
    {
        if (LightCandlePrompt != null)
        {
            LightCandlePrompt.gameObject.SetActive(show);

            if (show)
            {
                LightCandlePrompt.text = "Press E to light the candle!";
            }
        }
    }

    // Sets the current candle and updates the prompt visibility
    public void SetCurrentCandle(GameObject candle)
    {
        currentCandle = candle;

        // Only show prompt if candle exists and is not already lit
        ShowCandlePrompt(candle != null && !candle.GetComponent<CandleController>().isLit);
    }

    // Clears the current candle and hides the prompt
    public void ClearCurrentCandle()
    {
        currentCandle = null;
        ShowCandlePrompt(false);
    }

    // Hides the candle prompt after lighting
    public void HidePromptAfterLighting()
    {
        LightCandlePrompt.gameObject.SetActive(false);
    }

    // Displays a cutscene prompt with a typing effect
    public void ShowCutscenePromptTyped(string message, float delayBetweenLetters = 0.05f, float visibleDurationAfterTyping = 2f)
    {
        if (AmarealCutScene != null)
        {
            // Stop any currently running coroutines (prevents overlapping text)
            StopAllCoroutines();

            // Start typing animation
            StartCoroutine(TypeCutscenePrompt(message, delayBetweenLetters, visibleDurationAfterTyping));
        }
    }

    // Coroutine that types out a message letter-by-letter
    private IEnumerator TypeCutscenePrompt(string message, float delayBetweenLetters, float visibleDuration)
    {
        AmarealCutScene.gameObject.SetActive(true);
        AmarealCutScene.text = "";

        // Loop through each character and append it with delay
        foreach (char letter in message.ToCharArray())
        {
            AmarealCutScene.text += letter;
            yield return new WaitForSeconds(delayBetweenLetters);
        }

        // Keep text visible for a short duration after typing finishes
        yield return new WaitForSeconds(visibleDuration);

        // Hide the cutscene text
        AmarealCutScene.gameObject.SetActive(false);
    }

    // Coroutine to hide cutscene prompt after a delay (currently unused)
    private IEnumerator HideCutscenePrompt(float delay)
    {
        yield return new WaitForSeconds(delay);
        AmarealCutScene.gameObject.SetActive(false);
    }

    // Sets the player's checkpoint and challenge reset data
    public void SetCheckpoint(Vector3 newCheckpoint, Vector3 challengeReset, Transform challengeObject)
    {
        respawnPosition = newCheckpoint;
        challengePosition = challengeReset;
        challenge = challengeObject;
    }

    // Returns the current respawn position
    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }

    // Resets the challenge object to its original position
    public void ResetChallenge()
    {
        challenge.position = challengePosition;
    }

    /*
    // Previously used end prompt system (commented out)
    public void ShowEndPrompt(bool show)
    {
        EndPrompt.gameObject.SetActive(show);
        EndPrompt.text = "You have completed the first challenge! Press SPACE to restart!";
    }
    */

    // Called once per frame
    void Update()
    {
        // If restart is allowed and player presses R, restart the game
        if (canRestart && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Shows or hides the end UI panel
    public void ShowEndPrompt(bool show)
    {
        ENDUIPANEL.SetActive(show);

        if (show)
        {
            // Allow restart when end screen is shown
            canRestart = true;
        }
        else
        {
            // Disable restart when hidden
            canRestart = false;
        }
    }

    // Reloads the current scene
    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
