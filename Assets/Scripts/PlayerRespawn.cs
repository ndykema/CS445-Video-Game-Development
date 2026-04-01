using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles player death and respawning.
// If the player falls below a certain height, a death screen appears.
// Pressing Space after death respawns the player at the last checkpoint.
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenUI; // Death UI screen
    [SerializeField] private Rigidbody rb; // Reference to the player's Rigidbody
    [SerializeField] private float deathHeight = -10f; // Height below which the player dies

    /*
    // Optional challenge reset references that are currently not in use
    [SerializeField] private Transform Challenge;
    [SerializeField] private Vector3 challengeReset;
    */

    [SerializeField] private ResetManager resetManager; // Reference used to reset rocks or other challenge objects

    // Tracks whether the player is currently dead
    private bool isDead = false;

    void Update()
    {
        // Check if the player has fallen below the specified death height
        if (!isDead && transform.position.y < deathHeight)
        {
            Die();
        }

        // If the player is dead and presses Space, respawn them at the checkpoint
        if (isDead && Input.GetKeyDown(KeyCode.Space))
        {
            RespawnAtCheckpoint();
        }
    }

    // Called when the player dies
    public void Die()
    {
        // Mark the player as dead so death logic does not repeat
        isDead = true;

        // Stop the player's movement so they do not continue falling
        rb.velocity = Vector3.zero;

        // Show the death screen UI
        deathScreenUI.SetActive(true);
    }

    // Respawns the player at the saved checkpoint position
    public void RespawnAtCheckpoint()
    {
        // Move the player to the respawn position stored in GameBehavior
        transform.position = GameBehavior.Instance.GetRespawnPosition();

        // Reset the player's velocity so they start still
        rb.velocity = Vector3.zero;

        // If torch restoration is needed after respawning, this logic could be re-enabled
        /*
        if (GameBehavior.Instance.HasTorch)
        {
            GetComponent<TorchPickup>().GiveTorch();
        }
        */

        // Optional challenge reset hook, currently commented out
        // GameBehavior.Instance.ResetChallenge();

        // Reset all falling rocks or related challenge objects
        resetManager.ResetAllRocks();

        // Log respawn event to the Unity Console
        Debug.Log("Player Respawning!");

        // Hide the death screen UI
        deathScreenUI.SetActive(false);

        // Mark the player as alive again
        isDead = false;
    }
}
