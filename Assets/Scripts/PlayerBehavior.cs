using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script tracks player-triggered events, such as reaching a checkpoint.
// It uses a scene status value to ensure certain actions only occur once.
public class PlayerBehavior : MonoBehaviour
{
    // A float value that could be used for UI transparency or other effects (currently unused)
    private float alpha = 0f;

    // Tracks the current state of the scene (used to prevent repeated triggers)
    public float sceneStatus;

    // Called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the scene has not already reached status 1
        if (sceneStatus != 1)
        {
            // Update the scene status to indicate the checkpoint has been reached
            sceneStatus = 1;

            // Call the method to handle checkpoint messaging or logic
            checkpointMessage();
        }
    }

    // Method intended to handle checkpoint-related behavior (currently empty)
    private void checkpointMessage()
    {
        
    }
}
