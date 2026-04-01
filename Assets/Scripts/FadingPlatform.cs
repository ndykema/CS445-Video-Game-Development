using System.Collections;
using UnityEngine;

// This script controls a platform that repeatedly appears and disappears.
// It toggles both the visual (Renderer) and physical (Collider) components on a timer.
public class FadingPlatform : MonoBehaviour
{
    // How long the platform stays visible and solid
    public float visibleTime = 2f;

    // How long the platform stays invisible and non-collidable
    public float invisibleTime = 2f;

    // Reference to the Renderer component (controls visibility)
    private Renderer platformRenderer;

    // Reference to the Collider component (controls physical interaction)
    private Collider platformCollider;

    void Start()
    {
        // Get the Renderer attached to this GameObject
        platformRenderer = GetComponent<Renderer>();

        // Get the Collider attached to this GameObject
        platformCollider = GetComponent<Collider>();

        // Start the coroutine that handles toggling visibility and collision
        StartCoroutine(TogglePlatform());
    }

    // Coroutine that continuously toggles the platform on and off
    IEnumerator TogglePlatform()
    {
        // Infinite loop to keep the platform cycling
        while (true)
        {
            // Enable the platform (visible and solid)
            platformRenderer.enabled = true;
            platformCollider.enabled = true;

            // Wait for the specified visible time
            yield return new WaitForSeconds(visibleTime);

            // Disable the platform (invisible and non-solid)
            platformRenderer.enabled = false;
            platformCollider.enabled = false;

            // Wait for the specified invisible time
            yield return new WaitForSeconds(invisibleTime);
        }
    }
}
