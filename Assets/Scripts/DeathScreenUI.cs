using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenUI : MonoBehaviour
{
    public GameObject deathScreen;
    
    public PlayerRespawn playerRespawn; // Drag your Player object here

    private void Start()
    {
        deathScreen.SetActive(false); // Ensures it's off when the game starts
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f; // Pause game (optional)
    }

}