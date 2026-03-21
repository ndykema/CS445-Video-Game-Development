using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

 public class CandleController : MonoBehaviour
    {
        public bool isLit = false;
        public ParticleSystem flameParticles;
        public Light candleLight;
        public CinemachineVirtualCamera focusCam;
        public float focusDuration = 2.5f;
    public float audioDelay = 2f;

        public AudioSource torchAudio;

    // Reference to the other torch down the hall
    public GameObject checkpointLight;

        private void Start()
        {
            if (flameParticles != null)
            {
                flameParticles.Stop();
            }

            if (candleLight != null)
            {
                candleLight.enabled = false;
            }

            if (torchAudio != null)
             {
                 torchAudio.playOnAwake = false;
            }
    }

        public void LightCandle()
        {
            if (!isLit)
            {
                isLit = true;

                if (flameParticles != null) flameParticles.Play();
                if (candleLight != null) candleLight.enabled = true;

            StartCoroutine(PlayTorchSoundWithDelay(audioDelay)); // 🔥 Delay in seconds

            if (checkpointLight != null)
                {
                    CheckpointLight torchController = checkpointLight.GetComponent<CheckpointLight>();
                    if (torchController != null)
                    {
                        torchController.ActivateLight();
                    }
                }

            if (focusCam != null)
            {
                StartCoroutine(FocusOnTorch());
            }

            Debug.Log("Candle is lit!");
            }
        }
        private IEnumerator FocusOnTorch()
        {
        focusCam.Priority = 20; // Override main camera
        yield return new WaitForSeconds(focusDuration);
        focusCam.Priority = 0; // Return to main camera
     }

    private IEnumerator PlayTorchSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (torchAudio != null)
            torchAudio.Play();
    }
}
