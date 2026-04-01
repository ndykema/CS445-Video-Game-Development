using System.Collections;
using UnityEngine;

// This script manages audio transitions during a cutscene.
// It fades out the main music, then plays cutscene-specific music and ambient sounds.
public class CutsceneAudioManager : MonoBehaviour
{
    // Reference to the main background music AudioSource
    public AudioSource mainMusic;

    // AudioSource for cutscene music
    public AudioSource cutsceneMusic;

    // AudioSource for additional ambient sounds during the cutscene
    public AudioSource cutsceneAmbientSounds;

    // Duration (in seconds) for fading out the main music
    public float fadeDuration = 2f;

    // Public method to begin the cutscene audio sequence
    public void StartCutscene()
    {
        // Starts the coroutine that handles the transition
        StartCoroutine(HandleCutsceneAudio());
    }

    // Coroutine that controls the cutscene audio flow
    private IEnumerator HandleCutsceneAudio()
    {
        // Fade out the main background music over the specified duration
        yield return StartCoroutine(FadeOutAudio(mainMusic, fadeDuration));

        // Once the main music is faded out, start playing cutscene audio
        cutsceneMusic.Play();
        cutsceneAmbientSounds.Play();
    }

    // Coroutine to gradually fade out an AudioSource
    private IEnumerator FadeOutAudio(AudioSource audioSource, float fadeDuration)
    {
        // Store the original volume so it can be restored later
        float startVolume = audioSource.volume;

        // Continue reducing volume until it reaches 0
        while (audioSource.volume > 0f)
        {
            // Decrease volume over time based on fade duration
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;

            // Ensure volume does not go below 0
            audioSource.volume = Mathf.Max(audioSource.volume, 0f);

            // Wait until the next frame before continuing
            yield return null;
        }

        // Stop the audio once it has fully faded out
        audioSource.Stop();

        // Reset the volume to its original level for future use
        audioSource.volume = startVolume;
    }
}
