using Unity.VisualScripting;
using UnityEngine;

// This script controls player movement, including walking, jumping, and mouse-based camera rotation.
// It uses Rigidbody physics for movement and includes custom gravity and smooth camera handling.
public class PlayerMovement : MonoBehaviour
{
    // Movement speeds for strafing and forward/backward motion
    public float moveSpeed = 10f;
    public float moveSpeedForwards = 10f;

    // Force applied when the player jumps
    public float jumpForce = 15f;

    // Mouse sensitivity for looking around
    public float mouseSensitivity = 2f;

    // Friction applied when stopping (currently unused)
    public float stoppingFriction = 200f;

    // Speed for smoothing camera vertical rotation
    public float smoothRotationSpeed = 5f;

    // Custom gravity value applied when the player is airborne
    public float gravity = -9.81f;

    // Factor for quickly stopping movement (currently unused)
    public float quickStopFactor = 10f;

    // Tracks vertical velocity for gravity/jump logic
    private float verticalVelocity = 0f;

    // Input values for horizontal (A/D) and vertical (W/S) movement
    private float hInput;
    private float vInput;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    // Tracks vertical camera rotation (looking up/down)
    private float rotationX = 0f;

    // Tracks whether the player is on the ground
    private bool isGrounded;

    // Target transform for the camera to follow and rotate vertically
    public Transform cameraFollowTarget;

    private void Start()
    {
        // Get the Rigidbody component and prevent it from rotating due to physics
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Lock and hide the cursor for FPS-style camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Called once per frame
    void Update()
    {
        // Get movement input and scale by speed
        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        vInput = Input.GetAxis("Vertical") * moveSpeedForwards;

        // Handle different aspects of player control
        HandleMouseLook();
        HandleJump();
        HandleMovement();
    }

    // Handles player movement using Rigidbody
    void HandleMovement()
    {
        /*
            Old movement approach using separate MovePosition calls (commented out)
            rb.MovePosition(this.transform.position + this.transform.right * hInput * Time.fixedDeltaTime);
            rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        */

        // Calculate movement direction based on forward and sideways input
        Vector3 moveForwards = (transform.forward * vInput);
        Vector3 moveSide = (transform.right * hInput);
        Vector3 moveDirection = moveForwards + moveSide;

        // Move the player using Rigidbody while respecting physics timing
        rb.MovePosition(this.transform.position + moveDirection * Time.fixedDeltaTime);
    }

    // Handles mouse input for looking around
    void HandleMouseLook()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Adjust vertical rotation (looking up/down)
        rotationX -= mouseY;

        // Clamp vertical rotation to prevent flipping
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Smoothly rotate the camera target for vertical movement
        if (cameraFollowTarget != null)
        {
            Quaternion targetRotation = Quaternion.Euler(rotationX, 0f, 0f);

            // Smooth interpolation for more fluid camera motion
            cameraFollowTarget.localRotation = Quaternion.Slerp(
                cameraFollowTarget.localRotation,
                targetRotation,
                smoothRotationSpeed * Time.deltaTime
            );
        }
    }

    // Handles jumping and gravity behavior
    void HandleJump()
    {
        // Check if the player is grounded using a downward raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // If grounded and not moving upward, reset vertical velocity
        if (isGrounded && rb.velocity.y <= 0f)
        {
            verticalVelocity = 0f;
        }

        // If jump button is pressed while grounded, apply upward force
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // Apply gravity manually when the player is airborne
        if (!isGrounded)
        {
            rb.velocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }
    }
}
