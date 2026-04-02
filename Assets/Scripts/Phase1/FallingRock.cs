using UnityEngine;

// This script controls a falling rock that can be reset to its original position.
// It stores the initial transform values and ensures the Rigidbody is properly managed.
public class FallingRock : MonoBehaviour
{
    // Stores the starting position of the rock
    private Vector3 startPosition;

    // Stores the starting rotation of the rock
    private Quaternion startRotation;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    private void Awake()
    {
        // Store the initial position and rotation of the rock
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Get the Rigidbody component if it exists
        rb = GetComponent<Rigidbody>();

        // If no Rigidbody is attached, add one dynamically
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Set the Rigidbody to kinematic so the rock does not fall at the start
        rb.isKinematic = true;
    }

    // Public method to reset the rock to its original state
    public void ResetRock()
    {
        // Reset the position and rotation to the stored starting values
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Set Rigidbody back to kinematic so it remains stationary
        rb.isKinematic = true;

        // Clear any remaining velocity to prevent unintended motion
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
