using System.Collections;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    public float visibleTime = 2f;
    public float invisibleTime = 2f;
    private Renderer platformRenderer;
    private Collider platformCollider;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
        StartCoroutine(TogglePlatform());
    }

    IEnumerator TogglePlatform()
    {
        while (true)
        {
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            platformRenderer.enabled = false;
            platformCollider.enabled = false;
            yield return new WaitForSeconds(invisibleTime);
        }
    }
}
