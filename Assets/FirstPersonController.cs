using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;            // Walking speed
    public float jumpForce = 5f;        // Jumping force
    public float mouseSensitivity = 2f; // Mouse sensitivity for looking around

    private Rigidbody rb;
    private Camera playerCamera;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        // Lock cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle looking around
        LookAround();

        // Handle jumping (space key)
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Handle movement
        MovePlayer();
    }

    void LookAround()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player horizontally
        transform.Rotate(0, mouseX, 0);

        // Rotate the camera vertically
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);  // Limit vertical look angle
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void MovePlayer()
    {
        // Get input for movement (WASD keys or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down

        // Movement direction relative to where the player is facing
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        Vector3 movement = move * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        // Apply jump force upwards
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        // Check if the player is touching the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}