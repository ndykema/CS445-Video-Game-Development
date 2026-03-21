using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static UnityEngine.Rendering.DebugUI;
public class CrystalMover : MonoBehaviour
{
    [Header("Crystal Settings")]
    public GameObject crystal;  // Reference to the crystal GameObject
    public Transform newCrystalPosition;  // The new platform to move the crystal to

    [Header("Text Prompts")]
    public string promptText;  // The text to show when the player enters the trigger area

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the player is the one triggering the collider
        {
            MoveCrystal();  // Move the crystal
            ShowTextPrompt();  // Show more text
        }
    }

    // Move the crystal to the new platform
    void MoveCrystal()
    {
        if (crystal != null && newCrystalPosition != null)
        {
            crystal.transform.position = newCrystalPosition.position;  // Move the crystal
        }
    }

    // Show the text prompt (can be hooked into your GameBehavior)
    void ShowTextPrompt()
    {
        // Assuming GameBehavior.Instance.ShowCutscenePromptTyped is being used for text prompts
        GameBehavior.Instance.ShowCutscenePromptTyped(promptText, 0.05f, 3f);  // Adjust timing as needed
    }

    private IEnumerator PlayCutscenePrompts()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(2f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Mortal soul... You stand upon the threshold of dreams, summoned by my final breath of hope.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("I am the last light in the hallowed skies...and you are the last hand I may guide.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Within this dream, you will be tested. Not by strength... but by the purity of your will.");

        yield return new WaitForSeconds(8f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Prove yourself across the rocks, and I shall arm you with the flame of the old stars. Fail, and you will be consumed by my power.");

        yield return new WaitForSeconds(8f);
        GameBehavior.Instance.ShowCutscenePromptTyped("I have witnessed the fall of empires, the crumbling of gods... do not make me witness the end of hope.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("May a single flame guide you...lest you fall into the abyss...");
    }
}
