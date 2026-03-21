using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    public TMP_Text ProgressText;
    public TMP_Text TorchPrompt;
    public TMP_Text AmarealCutScene;
    //public TMP_Text EndPrompt;
    public GameObject ENDUIPANEL;
    private bool canRestart = false;


    public Vector3 respawnPosition;
    public Vector3 challengePosition;
    public Transform challenge;

    public int TorchCount { get; private set; } = 0;
    public bool HasTorch => TorchCount > 0;
    public bool IsTorchLit { get; private set; } = false;

    private void Start()
    {
        ProgressText.gameObject.SetActive(false);
        AmarealCutScene.gameObject.SetActive(false);
        //EndPrompt.gameObject.SetActive(true);
        ENDUIPANEL.SetActive(false);
    }
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        ProgressText.gameObject.SetActive(true);

        // Hide the progress text after a short delay
        StartCoroutine(HideProgressText());
    }

    private IEnumerator HideProgressText()
    {
        yield return new WaitForSeconds(2f); // Delay for 2 seconds
        ProgressText.gameObject.SetActive(false); // Hide the text after 2 seconds
    }

    public void AddTorch (int amount = 1)
    {
        TorchCount += amount;
        Debug.Log($"Player now has {TorchCount} torch(es)");

        IsTorchLit = true; // Set the torch as lit when picked up
        UpdateScene("You picked up a torch!");

    }

    public void RemoveTorch (int amount = 1)
    {
        TorchCount = Mathf.Max(0, TorchCount - amount);
        Debug.Log($"Player now has {TorchCount} torch(es)");

        if (TorchCount == 0)
        {
            IsTorchLit = false;
        }
    }

  

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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


    public TMP_Text LightCandlePrompt;  // Reference to the UI text for showing the prompt
    private GameObject currentCandle = null;

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

    public void SetCurrentCandle(GameObject candle)
    {
        currentCandle = candle;
        ShowCandlePrompt(candle != null && !candle.GetComponent<CandleController>().isLit);  // Only show prompt if the candle isn't already lit
    }

    public void ClearCurrentCandle()
    {
        currentCandle = null;
        ShowCandlePrompt(false);  // Hide prompt when moving away
    }

    public void HidePromptAfterLighting()
    {
        LightCandlePrompt.gameObject.SetActive(false); // Hide the prompt after lighting
    }

   public void ShowCutscenePromptTyped(string message, float delayBetweenLetters = 0.05f, float visibleDurationAfterTyping = 2f)
    {
        if (AmarealCutScene != null)
        {
            StopAllCoroutines(); // Stop any previous typing
            StartCoroutine(TypeCutscenePrompt(message, delayBetweenLetters, visibleDurationAfterTyping));
        }
    }

    private IEnumerator TypeCutscenePrompt(string message, float delayBetweenLetters, float visibleDuration)
    {
        AmarealCutScene.gameObject.SetActive(true);
        AmarealCutScene.text = "";

        foreach (char letter in message.ToCharArray())
        {
            AmarealCutScene.text += letter;
            yield return new WaitForSeconds(delayBetweenLetters);
        }

        yield return new WaitForSeconds(visibleDuration);
        AmarealCutScene.gameObject.SetActive(false);
    }


    private IEnumerator HideCutscenePrompt(float delay)
    {
        yield return new WaitForSeconds(delay);
        AmarealCutScene.gameObject.SetActive(false);
    }

    public void SetCheckpoint(Vector3 newCheckpoint, Vector3 challengeReset, Transform challengeObject)
    {
        respawnPosition = newCheckpoint;
        challengePosition = challengeReset;
        challenge = challengeObject;
    }

    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
        
    }

    public void  ResetChallenge()
    {
        challenge.position = challengePosition;
    }

    /* public void ShowEndPrompt(bool show)
     {
         EndPrompt.gameObject.SetActive(show);
         EndPrompt.text = "You have completed the first challenge! Press SPACE to restart!";

     }
    */



    // Update is called once per frame
    void Update()
    {
        // Only allow restarting if canRestart is true
        if (canRestart && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();  // Restart the game when Space is pressed
        }
    }

    public void ShowEndPrompt(bool show)
    {
        ENDUIPANEL.SetActive(show);  // Show or hide the entire panel

        if (show)
        {
            
            canRestart = true;  // Allow restart when the UI is shown
        }
        else
        {
            canRestart = false;  // Disable restart when the UI is hidden
        }
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
