using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // The image component for fading
    public float fadeDuration = 1f; // Duration of fade effect

    void Start()
    {
        // Make sure the fade image is fully transparent initially
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
    }

    // Call this to fade the screen to black
    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f)); // Fade from transparent (0) to opaque (1)
    }

    // Call this to fade the screen back in
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f)); // Fade from opaque (1) to transparent (0)
    }

    // Coroutine to handle the fade effect
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor; // Ensure the final color is set
    }
}