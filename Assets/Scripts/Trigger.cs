using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script responds to collisions with the object it is attached to.
// When a collision occurs, it changes the object's color and logs a message.
public class Trigger : MonoBehaviour
{
    // Called automatically when this object collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Change the material color of this object's MeshRenderer to red
        GetComponent<MeshRenderer>().material.color = Color.red;

        // Output a message to the Unity Console indicating a collision occurred
        Debug.Log("Something hit me!");
    }
}
