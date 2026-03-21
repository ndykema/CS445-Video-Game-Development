using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVanish : MonoBehaviour
{
    public GameObject platform; // Assign the platform in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (platform != null)
            {
                platform.SetActive(false); // or Destroy(platform);
            }
        }
    }
}
