// TorchFlicker.cs
using UnityEngine;

// This script creates a flickering effect for a torch by randomly changing its light intensity.
// It repeatedly updates the intensity at a set interval to simulate natural flame behavior.
public class TorchFlicker : MonoBehaviour
{
    // Reference to the Light component attached to this GameObject
    private Light torchLight;

    // Minimum and maximum intensity values for the flicker effect
    public float minIntensity = 1f;
    public float maxIntensity = 2f;

    // How often (in seconds) the flicker effect updates
    public float flickerSpeed = 0.1f;

    private void Start()
    {
        // Get the Light component attached to this GameObject
        torchLight = GetComponent<Light>();

        // Repeatedly call the Flicker method at a fixed interval
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    // Method that randomly adjusts the light intensity to create a flicker effect
    private void Flicker()
    {
        // Ensure the Light component exists before modifying it
        if (torchLight != null)
        {
            // Set the light intensity to a random value within the defined range
            torchLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
