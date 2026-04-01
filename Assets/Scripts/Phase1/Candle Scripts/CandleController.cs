using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

// This script controls the behavior of a candle that can be lit by the player.
// It handles visuals (particles + light), delayed audio, triggering another light,
// and temporarily switching the camera focus to a specific target.
public class CandleController : MonoBehaviour
{
    // Tracks whether the candle has already been lit
    public bool isLit = false;

    // Particle system for the flame effect
    public ParticleSystem flameParticles;

    // Light component for the candle glow
    public Light candleLight;

    // Cinemachine camera used to focus on another object (e.g., torch)
    public CinemachineVirtualCamera focusCam;

    // How long the camera stays focused on the target
    public float focusDuration = 2.5f;

    // Delay before playing the torch audio after lighting the candle
    public float audioDelay = 2f;

    // Audio source for the torch sound
    public AudioSource torchAudio;

    // Reference to another light (e.g., torch down the hall) to activate
    public GameObject checkpointLight;

    private void Start()
    {
        // Ensure flame particles are stopped at the start
        if (flameParticles != null)
        {
            flameParticles.Stop();
        }

        // Ensure the candle light is off at the start
        if (candleLight != null)
        {
            candleLight.enabled = false;
        }

        // Prevent the torch audio from playing automatically on start
        if (torchAudio != null)
        {
            torchAudio.playOnAwake = false;
        }
    }

    // Method to light the candle
    public void LightCandle()
    {
        // Only allow lighting if it hasn't already been lit
        if (!isLit)
        {
            isLit = true;

            // Start flame particle effect
            if (flameParticles != null) flameParticles.Play();

            // Enable candle light
            if (candleLight != null) candleLight.enabled = true;

            // Play torch sound after a delay
            StartCoroutine(PlayTorchSoundWithDelay(audioDelay));

            // Activate the linked checkpoint light (e.g., another torch)
            if (checkpointLight != null)
            {
                CheckpointLight torchController = checkpointLight.GetComponent<CheckpointLight>();
                if (torchController != null)
                {
                    torchController.ActivateLight();
                }
            }

            // Trigger a temporary camera focus on the torch
            if (focusCam != null)
            {
                StartCoroutine(FocusOnTorch());
            }

            // Log confirmation to the console
            Debug.Log("Candle is lit!");
        }
    }

    // Coroutine to temporarily switch camera focus to the torch
    private IEnumerator FocusOnTorch()
    {
        // Increase priority so this camera takes control
        focusCam.Priority = 20;

        // Wait for the specified focus duration
        yield return new WaitForSeconds(focusDuration);

        // Lower priority to return control to the main camera
        focusCam.Priority = 0;
    }

    // Coroutine to play the torch audio after a delay
    private IEnumerator PlayTorchSoundWithDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Play the torch audio if it exists
        if (torchAudio != null)
            torchAudio.Play();
    }
}
