using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleTriggerInteractor : MonoBehaviour
{
    private GameObject currentCandle; // Store the current candle in range

    void Update()
    {
        // If we're near a candle and the player presses E, light the candle
        if (currentCandle != null && Input.GetKeyDown(KeyCode.E) && GameBehavior.Instance.IsTorchLit)
        {
            // Call your candle lighting method
            CandleController candleController = currentCandle.GetComponent<CandleController>();
            if (candleController != null && !candleController.isLit)
            {
                candleController.LightCandle();
                GameBehavior.Instance.HidePromptAfterLighting();  // Hide the prompt after lighting
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            currentCandle = other.gameObject;
            GameBehavior.Instance.SetCurrentCandle(currentCandle);  // Show the prompt
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            currentCandle = null;
            GameBehavior.Instance.ClearCurrentCandle();  // Hide the prompt
        }
    }
}