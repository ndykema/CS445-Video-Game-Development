using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls a checkpoint torch or light source.
// It starts unlit, then can be activated to play flame particles
// and smoothly fade the torch light in after a delay.
public class CheckpointLight : MonoBehaviour
{
    // Tracks whether this light has already been activated
    public bool isLit = false;

    // Particle system used for the flame effect
    public ParticleSystem flameParticles;

    // Light component for the torch glow
    public Light torchLight;

    // Amount of time used both for the delay and the light fade-in
    public float fadeDuration = 2f;

    private void Start()
    {
        // Make sure the flame particles are off when the scene starts
        if (flameParticles != null)
        {
            flameParticles.Stop();
        }

        // Make sure the light is off and starts at zero intensity
        if (torchLight != null)
        {
            torchLight.enabled = false;
            torchLight.intensity = 0f;
        }
    }

    // Public method used to activate the torch/light sequence
    public void ActivateLight()
    {
        // Only activate the torch if it has not already been lit
        if (!isLit)
        {
            isLit = true;
            StartCoroutine(LightTorchWithDelay());
        }
    }

    // Coroutine that waits, then lights the torch and fades in the light
    private IEnumerator LightTorchWithDelay()
    {
        // Wait before turning on the torch
        yield return new WaitForSeconds(fadeDuration);

        // Play the flame particle effect if assigned
        if (flameParticles != null)
        {
            flameParticles.Play();
        }

        // Enable and fade in the torch light if assigned
        if (torchLight != null)
        {
            torchLight.enabled = true;

            float elapsedTime = 0f;
            float startIntensity = 0f;
            float targetIntensity = 1f;

            // Gradually increase the light intensity over time
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                torchLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
                yield return null;
            }

            // Make sure the final intensity is set exactly
            torchLight.intensity = targetIntensity;
        }

        // Log confirmation to the Unity Console
        Debug.Log("Torch down the hall is now lit!");
    }
}
