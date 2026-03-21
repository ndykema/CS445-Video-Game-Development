using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenUI; // Death UI screen
    [SerializeField] private Rigidbody rb; // Reference to the player's Rigidbody
    [SerializeField] private float deathHeight = -10f; // Height below which the player dies
    /*
    [SerializeField] private Transform Challenge;
    [SerializeField] private Vector3 challengeReset;
    */
    [SerializeField] private ResetManager resetManager;

    private bool isDead = false;

    void Update()
    {
        // Check if the player has fallen below the specified height
        if (!isDead && transform.position.y < deathHeight)
        {
            Die();
        }

        // If the player is dead and presses Space, restart the game
        if (isDead && Input.GetKeyDown(KeyCode.Space))
        {
            RespawnAtCheckpoint();
        }
    }

    // Method that gets called when the player dies
    public void Die()
    {
        isDead = true;
        rb.velocity = Vector3.zero; // Stop the player from falling further
        deathScreenUI.SetActive(true); // Show the death screen

 
    }

    // Method to reset the game (reload the current scene)
    public void RespawnAtCheckpoint()
    {
        // Respawn at last checkpoint
        transform.position = GameBehavior.Instance.GetRespawnPosition();
      
        // Reset velocity
        rb.velocity = Vector3.zero;

        // If the torch was picked up, make sure the player has the torch again
        /*if (GameBehavior.Instance.HasTorch)
        {
            GetComponent<TorchPickup>().GiveTorch();
        }
        */
        // GameBehavior.Instance.ResetChallenge();

        resetManager.ResetAllRocks(); // Reset all the falling rocks
      

        Debug.Log("Player Respawning!");
        deathScreenUI.SetActive(false);
        isDead = false;
    }
}