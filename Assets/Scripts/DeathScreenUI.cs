using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script controls the behavior of the death screen UI.
// It allows the screen to be hidden at start and shown when the player dies.
public class DeathScreenUI : MonoBehaviour
{
    // Reference to the death screen UI GameObject
    public GameObject deathScreen;
    
    // Reference to the PlayerRespawn script (assigned in Inspector)
    public PlayerRespawn playerRespawn;

    private void Start()
    {
        // Ensure the death screen is hidden when the game begins
        deathScreen.SetActive(false);
    }

    // Method to display the death screen
    public void ShowDeathScreen()
    {
        // Enable the death screen UI
        deathScreen.SetActive(true);

        // Pause the game by stopping time (optional behavior)
        Time.timeScale = 0f;
    }
}
