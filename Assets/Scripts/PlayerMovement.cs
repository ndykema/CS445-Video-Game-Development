using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float moveSpeedForwards = 10f;
    public float jumpForce = 15f;
    public float mouseSensitivity = 2f;
    public float stoppingFriction = 200f;
    public float smoothRotationSpeed = 5f; // Speed at which the camera rotates to follow the player
    public float gravity = -9.81f;  // Custom gravity value
    public float quickStopFactor = 10f;  // The factor by which the character quickly stops when keys are released
    private float verticalVelocity = 0f; // To track the vertical movement (up/down)

    private float hInput;
    private float vInput;
    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded;
    public Transform cameraFollowTarget; // Empty object in front of the player for the camera to follow
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {

        hInput = Input.GetAxis("Horizontal") * moveSpeed;
        vInput = Input.GetAxis("Vertical") * moveSpeedForwards;
        HandleMouseLook();
        HandleJump();
        HandleMovement();
    }

    void HandleMovement()
    {
        /*
                rb.MovePosition(this.transform.position + this.transform.right * hInput * Time.fixedDeltaTime);
                rb.MovePosition(this.transform.position + this.transform.forward * vInput *Time.fixedDeltaTime);
        */
        Vector3 moveForwards = (transform.forward * vInput);
        Vector3 moveSide = (transform.right * hInput);
        Vector3 moveDirection = moveForwards + moveSide;
        rb.MovePosition(this.transform.position + moveDirection * Time.fixedDeltaTime);
    }
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX); // Rotate the player body left and right

        // Clamping the vertical rotation
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Smooth the camera's up/down movement for a more fluid experience
        if (cameraFollowTarget != null)
        {
            Quaternion targetRotation = Quaternion.Euler(rotationX, 0f, 0f);
            cameraFollowTarget.localRotation = Quaternion.Slerp(cameraFollowTarget.localRotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
        }
    }
    void HandleJump()
    {
        // Check if the player is grounded using a raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isGrounded && rb.velocity.y <= 0f)
        {
            // Reset vertical velocity when on the ground to avoid continuous falling
            verticalVelocity = 0f;
        }

        // If grounded and jump button is pressed, apply the jump force
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply the jump force, adjusting the Y velocity for the jump height
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // Apply gravity if the player is not grounded
        if (!isGrounded)
        {
            // Apply gravity over time for a falling effect
            rb.velocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }
    }

}






