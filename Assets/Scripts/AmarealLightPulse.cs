using UnityEngine;

public class LightPulseWave : MonoBehaviour
{
    public Light lightSource;
    public float pulseSpeed = 2f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float minRange = 5f;
    public float maxRange = 10f;

    private void Update()
    {
        float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, wave);
        lightSource.range = Mathf.Lerp(minRange, maxRange, wave);
    }
}