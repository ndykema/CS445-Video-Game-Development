// TorchFlicker.cs
using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    private Light torchLight;
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 0.1f;

    private void Start()
    {
        torchLight = GetComponent<Light>();
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    private void Flicker()
    {
        if (torchLight != null)
        {
            torchLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}