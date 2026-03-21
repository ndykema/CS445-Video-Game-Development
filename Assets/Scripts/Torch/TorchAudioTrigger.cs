using UnityEngine;


public class TorchAudioTrigger : MonoBehaviour
{
    private AudioSource torchAudio;

    private void Start()
    {
        torchAudio = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!torchAudio.isPlaying)
                torchAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (torchAudio.isPlaying)
                torchAudio.Stop();
        }
    }
}