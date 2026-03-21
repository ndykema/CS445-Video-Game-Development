using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLight : MonoBehaviour
{
    public bool isLit = false;
    public ParticleSystem flameParticles;
    public Light torchLight;
    public float fadeDuration = 2f;

    private void Start()
    {
        // Make sure everything is off at the start
        if (flameParticles != null)
        {
            flameParticles.Stop();
        }

        if (torchLight != null)
        {
            torchLight.enabled = false;
            torchLight.intensity = 0f;
        }
    }

    public void ActivateLight()
    {
        if (!isLit)
        {
            isLit = true;
            StartCoroutine(LightTorchWithDelay());
        }
    }

    private IEnumerator LightTorchWithDelay()
    {
        yield return new WaitForSeconds(fadeDuration);

        if (flameParticles != null)
        {
            flameParticles.Play();
        }

        if (torchLight != null)
        {
            torchLight.enabled = true;
            float elapsedTime = 0f;
            float startIntensity = 0f;
            float targetIntensity = 1f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                torchLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
                yield return null;
            }

            torchLight.intensity = targetIntensity;
        }

        Debug.Log("Torch down the hall is now lit!");
    }
}

