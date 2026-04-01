using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script tracks how many times the object collides with other objects.
// Each collision increments a score counter and logs it to the console.
public class Score : MonoBehaviour
{
    // Stores the number of collisions that have occurred
    int score = 0;

    // Called automatically when this object collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Increase the score count by 1 for each collision
        score++;

        // Output the current score to the Unity Console
        Debug.Log("You have bumped into items, this many times: " + score);
    }
}
