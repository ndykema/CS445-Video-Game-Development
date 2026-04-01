using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows the player to interact with candles when within range.
// When near a candle, pressing "E" will light it (if the player has a lit torch).
public class CandleTriggerInteractor : MonoBehaviour
{
    // Stores the current candle GameObject the player is near
    private GameObject currentCandle;

    void Update()
    {
        // If a candle is in range, the player presses E, and the torch is lit
        if (currentCandle != null && Input.GetKeyDown(KeyCode.E) && GameBehavior.Instance.IsTorchLit)
        {
            // Get the CandleController component from the current candle
            CandleController candleController = currentCandle.GetComponent<CandleController>();

            // If the candle exists and is not already lit
            if (candleController != null && !candleController.isLit)
            {
                // Light the candle
                candleController.LightCandle();

                // Hide the interaction prompt after lighting
                GameBehavior.Instance.HidePromptAfterLighting();
            }
        }
    }

    // Called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entered is tagged as a candle
        if (other.CompareTag("Candle"))
        {
            // Store the candle reference
            currentCandle = other.gameObject;

            // Notify GameBehavior to show a prompt for interaction
            GameBehavior.Instance.SetCurrentCandle(currentCandle);
        }
    }

    // Called when the player exits a trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exited is tagged as a candle
        if (other.CompareTag("Candle"))
        {
            // Clear the current candle reference
            currentCandle = null;

            // Notify GameBehavior to remove the interaction prompt
            GameBehavior.Instance.ClearCurrentCandle();
        }
    }
}
