using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GameObject parentObject; // The empty parent containing all falling rocks

    // Call this method to reset all falling rocks under the parent
    public void ResetAllRocks()
    {
        // Loop through all child objects (rocks) under the parent object
        foreach (Transform child in parentObject.transform)
        {
            // Check if the child object has the FallingRock script attached
            FallingRock fallingRock = child.GetComponent<FallingRock>();

            if (fallingRock != null)
            {
                // Reset the rock using the FallingRock script
                fallingRock.ResetRock();
                Debug.Log("Rock reset: " + child.name);
            }
            else
            {
                Debug.LogWarning("FallingRock script not found on " + child.name);
            }
        }

        Debug.Log("All rocks under the parent have been reset!");
    }
}
