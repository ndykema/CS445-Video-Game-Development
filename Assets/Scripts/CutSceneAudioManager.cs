using System.Collections;
using UnityEngine;

public class CutsceneAudioManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource cutsceneMusic;
    public AudioSource cutsceneAmbientSounds;
    public float fadeDuration = 2f;

    public void StartCutscene()
    {
        StartCoroutine(HandleCutsceneAudio());
    }

    private IEnumerator HandleCutsceneAudio()
    {
        // Fade out main music
        yield return StartCoroutine(FadeOutAudio(mainMusic, fadeDuration));

        // Play cutscene music and ambient sounds
        cutsceneMusic.Play();
        cutsceneAmbientSounds.Play();
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            audioSource.volume = Mathf.Max(audioSource.volume, 0f); // Clamp to 0
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume so it’s ready if replayed
    }
}
