using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float alpha = 0f;

    public float sceneStatus;
    private void OnTriggerEnter(Collider other)
    {
        if (sceneStatus != 1)
        {
            sceneStatus = 1;
            checkpointMessage();
        }
    }

    private void checkpointMessage()
    {
        
    }
}
