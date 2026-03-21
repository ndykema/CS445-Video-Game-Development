using UnityEngine;

public class CompletionTrigger : MonoBehaviour
{
    public GameBehavior gameBehavior;  // Reference to the GameBehavior script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the end prompt when the player enters the trigger
            gameBehavior.ShowEndPrompt(true);  // Show the "you've completed the challenge" message
        }
    }
}
