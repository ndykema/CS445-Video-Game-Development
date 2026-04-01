using UnityEngine;

// This script creates a pulsing light effect using a sine wave.
// It smoothly changes both the intensity and range of a Light component over time.
public class LightPulseWave : MonoBehaviour
{
    // Reference to the Light component that will be affected
    public Light lightSource;

    // Controls how fast the pulsing effect occurs
    public float pulseSpeed = 2f;

    // Minimum and maximum brightness of the light
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    // Minimum and maximum range (distance the light reaches)
    public float minRange = 5f;
    public float maxRange = 10f;

    private void Update()
    {
        // Create a sine wave that oscillates between 0 and 1 over time
        float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

        // Smoothly interpolate the light's intensity based on the wave value
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, wave);

        // Smoothly interpolate the light's range based on the wave value
        lightSource.range = Mathf.Lerp(minRange, maxRange, wave);
    }
}
