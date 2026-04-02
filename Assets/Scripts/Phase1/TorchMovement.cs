using UnityEngine;

// This script creates a bobbing (floating up and down) effect for an object,
// such as a torch, using a sine wave for smooth motion.
public class TorchBobber : MonoBehaviour
{
    // Speed at which the object bobs up and down
    public float bobSpeed = 1f;

    // Height (amplitude) of the bobbing motion
    public float bobHeight = 0.1f;

    // Determines whether the bobbing effect is active
    public bool isActive = true;

    // Stores the starting local position of the object
    private Vector3 startPos;

    void Start()
    {
        // Record the initial local position so movement is relative
        startPos = transform.localPosition;
    }

    void Update()
    {
        // If bobbing is disabled, exit early
        if (!isActive) return;

        // Calculate vertical offset using a sine wave for smooth oscillation
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Apply the vertical offset to the object's local position
        transform.localPosition = startPos + new Vector3(0f, newY, 0f);
    }
}
