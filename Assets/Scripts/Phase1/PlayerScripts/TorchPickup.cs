using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;
using TMPro;
public class TorchPickup : MonoBehaviour
{
    [Header("Torch Settings")]
    public GameObject torchPrefab;
    public Transform torchHoldPoint;
    private GameObject heldTorch;

    [Header("Cinematic Settings")]
    public PlayableDirector cutsceneDirector;  // The Playable Director for cutscene
    public CinemachineVirtualCamera cutsceneCamera;  // The camera for the cutscene
    public CinemachineVirtualCamera playerCamera;    // The player camera

    [Header("Audio Settings")]
    public AudioSource mainMusic;
    public AudioSource cutsceneMusic;
    public float musicFadeSpeed = 1f;

    [Header("New Object Settings")]
    public GameObject newObjectToAppear;  // The object to appear after picking up the torch
    [SerializeField] float AmarealAppearDelay = 1.5f;

    [Header("Amareal EP")]
    public GameObject AmarealEP;
    

    [Header("Player Respawn Settings")]
    public GameObject player;
    public GameObject challenge;
    public Transform challengeObject;


    void Start()
    {
        // Ensure the new object is not visible at the start
        if (newObjectToAppear != null)
        {
            newObjectToAppear.SetActive(false);  // Deactivate the object initially
        }
        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnd;
        }
        if (AmarealEP != null)
        {
            AmarealEP.SetActive(false);  // Deactivate the object initially
        }

    }

    void Update()
    {
        ShowTorchPromptNearbyTorch();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupTorch();
        }
    }

    void TryPickupTorch()
    {
        if (heldTorch != null) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Torch"))
            {
                // Pick up the torch
                GiveTorch();
                Destroy(hit.gameObject); // Remove the torch in the world

                // Hide the UI prompt
                GameBehavior.Instance.ShowTorchPrompt(false);
                GameBehavior.Instance.AddTorch();

                // Destroy TorchBobber if it exists
                GameObject torch = GameObject.Find("Torch(Clone)");
                if (torch != null)
                {
                    TorchBobber bobber = torch.GetComponent<TorchBobber>();
                    if (bobber != null)
                    {
                        Destroy(bobber);
                    }
                }
                
                // Start the cinematic cutscene
                StartCutscene();

                // Activate the new object after the torch is picked up
                if (newObjectToAppear != null)
                {
                    StartCoroutine(DelayedObjectAppear());
                }

                GameBehavior.Instance.SetCheckpoint(player.transform.position,challenge.transform.position,challengeObject);

                break;
            }
        }
    }

    void ShowTorchPromptNearbyTorch()
    {
        if (GameBehavior.Instance.HasTorch)
        {
            GameBehavior.Instance.ShowTorchPrompt(false);
            return;
        }
        bool nearTorch = false;

        Collider[] hits = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Torch"))
            {
                nearTorch = true;
                break;
            }
        }

        GameBehavior.Instance.ShowTorchPrompt(nearTorch);
    }

    void StartCutscene()
    {
        StartCoroutine(FadeOutAudio(mainMusic, musicFadeSpeed));
        StartCoroutine(FadeInAudio(cutsceneMusic, musicFadeSpeed));

        cutsceneCamera.Priority = 100;
        playerCamera.Priority = 0;

        // Subscribe right before playing
        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnd;
            cutsceneDirector.Play();
            StartCoroutine(PlayCutscenePrompts());
            Debug.Log("CutScene Started!");
        }

        
    }


    // Fade out the audio over time
    private IEnumerator FadeOutAudio(AudioSource audioSource, float speed)
    {
        if (audioSource == null) yield break;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * speed;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f; // Reset for future use
    }

    // Fade in the audio over time
    private IEnumerator FadeInAudio(AudioSource audioSource, float speed)
    {
        if (audioSource == null) yield break;

        audioSource.Play();
        audioSource.volume = 0f;

        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime * speed;
            yield return null;
        }
    }

    // When the cutscene ends, reset everything
    void OnCutsceneEnd(PlayableDirector director)
    {
        Debug.Log("Cutscene finished, OnCutsceneEnd is being called.");

        // Check if it's the right director that finished
        if (director == cutsceneDirector)
        {
            cutsceneCamera.Priority = 0;
            playerCamera.Priority = 10;

            // Fade out the cutscene music and fade in main music
            StartCoroutine(FadeOutAudio(cutsceneMusic, musicFadeSpeed));
            mainMusic.Play();

            // Clean up the subscription to avoid bugs
            cutsceneDirector.stopped -= OnCutsceneEnd;

            StartCoroutine(AmarealDisappear());
            StartCoroutine(AmarealNewPosition());
        }
        else
        {
            Debug.LogWarning("The director that stopped wasn't the expected cutscene director.");
        }
    }


    private IEnumerator DelayedObjectAppear()
    {
        yield return new WaitForSeconds(AmarealAppearDelay);
        newObjectToAppear.SetActive(true);
        
    }

    private IEnumerator AmarealDisappear()
    {
        yield return new WaitForSeconds(AmarealAppearDelay);
        newObjectToAppear.SetActive(false);
    }
    
    private IEnumerator AmarealNewPosition()
    {
        yield return new WaitForSeconds(AmarealAppearDelay);
        AmarealEP.SetActive(true);
        

    }

    private IEnumerator PlayCutscenePrompts()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(2f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Mortal soul... You stand upon the threshold of dreams, summoned by my final breath of hope.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("I am the last light in the hallowed skies...and you are the last hand I may guide.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Within this dream, you will be tested. Not by strength... but by the purity of your will.");

        yield return new WaitForSeconds(8f);
        GameBehavior.Instance.ShowCutscenePromptTyped("Prove yourself across the rocks, and I shall arm you with the flame of the old stars. Fail, and you will be consumed by my power.");

        yield return new WaitForSeconds(8f);
        GameBehavior.Instance.ShowCutscenePromptTyped("I have witnessed the fall of empires, the crumbling of gods... do not make me witness the end of hope.");

        yield return new WaitForSeconds(7f);
        GameBehavior.Instance.ShowCutscenePromptTyped("May a single flame guide you...lest you fall into the abyss...");
    }

    public void GiveTorch()
    {
        heldTorch = Instantiate(torchPrefab, torchHoldPoint.position, torchHoldPoint.rotation);
        heldTorch.transform.SetParent(torchHoldPoint);
    }
}
