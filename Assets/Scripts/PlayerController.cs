using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float sensitivity = 2f;

    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public CinemachineCamera cinemachineCamera;

    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isRunning;
    private bool isJumping;
    private float xRotation = 0f;
    private Vector3 velocity;
    private float gravity = -9.81f;

    private bool isQuitting;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Sprint.performed += ctx => isRunning = true;
        controls.Player.Sprint.canceled += ctx => isRunning = false;

        controls.Player.Jump.performed += ctx => { if (controller.isGrounded) isJumping = true; };

        controls.Player.Escape.performed += ctx => isQuitting = true;
        controls.Player.Escape.canceled += ctx => isQuitting = false;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        Move();
        Look();
        QuitGame();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        float speed = isRunning ? runSpeed : walkSpeed;

        controller.Move(moveDirection * speed * Time.deltaTime);

        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJumping = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void QuitGame()
    {
        if (isQuitting)
        {
            EditorApplication.ExitPlaymode();
        }
    }
}
