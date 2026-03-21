using UnityEngine;

public class FallingRock : MonoBehaviour
{
    private Vector3 startPosition;            // Store the starting position of the rock
    private Quaternion startRotation;         // Store the starting rotation of the rock
    private Rigidbody rb;                     // Reference to the Rigidbody component

    private void Awake()
    {
        // Store the initial position and rotation of the rock
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Get the Rigidbody component if it exists, or add one if not
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Initially set the Rigidbody to kinematic (so it doesn't fall when the game starts)
        rb.isKinematic = true;
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