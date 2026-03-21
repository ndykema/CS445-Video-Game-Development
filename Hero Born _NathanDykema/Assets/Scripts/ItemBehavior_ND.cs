using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ItemBehavior_ND : MonoBehaviour
{
    public GameBehavior gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Destroy(this.transform.parent.gameObject);

                Debug.Log("Item collected!");

            }
       gameManager.Items += 1;

        gameManager.PrintLootReport();
    }
    
}
