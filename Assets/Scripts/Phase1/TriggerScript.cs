using UnityEngine;
using System.Collections;
using Cinemachine;

public class TriggerScript: MonoBehaviour
{
    public GameObject platform;               // Assign in Inspector
    public AudioClip dropSound;               // Assign drop sound
    public CinemachineImpulseSource impulse;  // Assign the impulse source component

    private Vector3 startPosition;            // To store the starting position of the rock
    private Quaternion startRotation;         // To store the starting rotation of the rock
    private Rigidbody rb;                     // Reference to the Rigidbody

    private void Awake()
    {
        // Store the initial position and rotation of the rock
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Get the Rigidbody component if it exists, or add one if not
        rb = platform.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = platform.AddComponent<Rigidbody>();
        }

        // Initially set the Rigidbody to kinematic (so it doesn't fall when the game starts)
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DropPlatformAfterDelay(0.25f));
        }
    }

    IEnumerator DropPlatformAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 🔊 Play drop sound
        if (dropSound != null)
        {
            AudioSource.PlayClipAtPoint(dropSound, platform.transform.position);
        }

        // 💥 Trigger Cinemachine Impulse (camera shake)
        if (impulse != null)
        {
            impulse.GenerateImpulse();
        }

        // ⬇️ Drop platform by enabling physics (set isKinematic to false)
        rb.isKinematic = false;

        // Reset the velocity and angular velocity to stop any residual motion
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // Call this function to reset the rock to its original state
    public void ResetRock()
    {
        // Reset the position and rotation to the start values
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Put the Rigidbody back to kinematic (it won't fall)
        rb.isKinematic = true;

        // Reset any residual velocities
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
