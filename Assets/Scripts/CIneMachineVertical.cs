using Cinemachine;
using UnityEngine;


public class CameraVerticalLook : MonoBehaviour
{
    public float mouseSensitivity = 3f;  // Controls how fast the camera rotates based on mouse input
    public float verticalClampMin = -90f; // Minimum angle for up/down rotation
    public float verticalClampMax = 90f;  // Maximum angle for up/down rotation

    private float rotationX = 0f;  // Stores the current vertical rotation
    private CinemachineVirtualCamera Camera2;  // Reference to the Cinemachine Virtual Camera
    private Transform Player;  // Reference to the player's body for horizontal rotation
    public Transform cameraFollowTarget;
    

    private bool isMouseMoved = false;  // Flag to track if the mouse has moved

    void Start()
    {

        if (Camera2 != null)
        {
            Camera2.Follow = cameraFollowTarget;
            Camera2.LookAt = cameraFollowTarget;
        }

        // Lock the cursor at the start to avoid mouse movement influencing anything
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get the Cinemachine Virtual Camera and player body (if applicable)
        Camera2 = GetComponent<CinemachineVirtualCamera>();
        Player = Camera2.Follow; // Assuming the player body is the Follow target
        Camera2.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor in the center
        Cursor.visible = false;  // Hide the cursor
    }

    void Update()
    {
        if (!isMouseMoved && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            // Switch the camera to follow the player when the mouse moves
            isMouseMoved = true;

            // Set the camera to now follow the player
            if (Camera2 != null)
            {
                Camera2.Follow = cameraFollowTarget;
                Camera2.LookAt = cameraFollowTarget;
            }
        }
        HandleVerticalLook();
    }

    void HandleVerticalLook()
    {
        // Get the vertical mouse movement input (up and down)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update the rotationX based on the mouseY input
        rotationX -= mouseY;

        // Clamp the vertical rotation to prevent the camera from flipping over
        rotationX = Mathf.Clamp(rotationX, verticalClampMin, verticalClampMax);

        // Apply the vertical rotation to the camera (only the up/down look)
        Camera2.transform.localRotation = Quaternion.Euler(rotationX, Camera2.transform.localRotation.eulerAngles.y, 0f);
    }
}