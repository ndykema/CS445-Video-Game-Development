using UnityEngine;

// This script controls torch audio based on player proximity.
// When the player enters the trigger, the torch sound plays.
// When the player exits, the sound stops.
public class TorchAudioTrigger : MonoBehaviour
{
    // Reference to the AudioSource on the parent object (likely the torch)
    private AudioSource torchAudio;

    private void Start()
    {
        // Get the AudioSource component from the parent GameObject
        torchAudio = GetComponentInParent<AudioSource>();
    }

    // Called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            // If the audio is not already playing, start it
            if (!torchAudio.isPlaying)
                torchAudio.Play();
        }
    }

    // Called when another collider exits this trigger
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            // If the audio is currently playing, stop it
            if (torchAudio.isPlaying)
                torchAudio.Stop();
        }
    }
}
