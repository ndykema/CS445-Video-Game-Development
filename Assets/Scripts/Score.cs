using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score = 0;
    void OnCollisionEnter(Collision collision)
    {
        score++;
        Debug.Log("You have bumped into items, this many times: "+score);
    }
  
}
