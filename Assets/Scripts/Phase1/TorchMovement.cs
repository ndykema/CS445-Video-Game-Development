using UnityEngine;

public class TorchBobber : MonoBehaviour
{
    public float bobSpeed = 1f;
    public float bobHeight = 0.1f;
    public bool isActive = true; // Controls if bobbing is active

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (!isActive) return;

        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = startPos + new Vector3(0f, newY, 0f);
    }

   
}