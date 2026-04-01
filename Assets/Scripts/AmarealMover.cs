using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static UnityEngine.Rendering.DebugUI;

// This script handles moving a crystal when the player enters a trigger,
// as well as displaying text prompts and optionally running a cutscene sequence.
public class CrystalMover : MonoBehaviour
{
    [Header("Crystal Settings")]
    // Reference to the crystal GameObject that will be moved
    public GameObject crystal;

    // The transform representing the new position (e.g., a platform) for the crystal
    public Transform newCrystalPosition;

    [Header("Text Prompts")]
    // Text that will be displayed when the player enters the trigger
    public string promptText;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            MoveCrystal();        // Move the crystal to the new position
            ShowTextPrompt();     // Display the initial text prompt
        }
    }

    // Moves the crystal to the assigned new position
    void MoveCrystal()
    {
        // Ensure both the crystal and target position are assigned
        if (crystal != null && newCrystalPosition != null)
        {
            // Set the crystal's position to the new platform's position
            crystal.transform.position = newCrystalPosition.position;
        }
    }

    // Displays a text prompt using the GameBehavior system
    void ShowTextPrompt()
    {
        // Calls a method (likely a singleton) to show typed cutscene-style text
        // Parameters: text, typing speed, duration on screen
        GameBehavior.Instance.ShowCutscenePromptTyped(promptText, 0.05f, 3f);
    }

    // Coroutine that plays a sequence of cutscene prompts with delays between them
    private IEnumerator PlayCutscenePrompts()
    {
        // Initial delay before starting the sequence
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(2f);

        // First line of dialogue
        GameBehavior.Instance.ShowCutscenePromptTyped("Mortal soul... You stand upon the threshold of dreams, summoned by my final breath of hope.");

        // Wait before showing next line
        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("I am the last light in the hallowed skies...and you are the last hand I may guide.");

        // Continue sequence with timed dialogue
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
