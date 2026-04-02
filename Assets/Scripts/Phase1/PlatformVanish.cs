using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script disables (or removes) a platform when the player enters a trigger.
// It can be used for traps, disappearing platforms, or puzzle mechanics.
public class PlatformVanish : MonoBehaviour
{
    // Reference to the platform GameObject that will be disabled
    public GameObject platform; // Assign the platform in the Inspector

    // Called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Ensure the platform reference exists before trying to disable it
            if (platform != null)
            {
                // Disable the platform (alternatively could use Destroy(platform))
                platform.SetActive(false);
            }
        }
    }
}
