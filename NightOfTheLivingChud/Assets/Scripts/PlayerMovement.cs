using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    public Slider staminaSlider; // Reference to the stamina slider UI element

    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrain = 25f;
    public float staminaRegen = 15f;

    private CharacterController controller;
    private float verticalLookRotation = 0f;
    private float gravity = -9.81f;
    private float verticalVelocity = 0f;

    void Start()
    {
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        LookAround();
        MovePlayer();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isMoving = moveX != 0 || moveZ != 0;
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift);

        // Must have at least 10 stamina to start running
        bool canRun = wantsToRun && isMoving && currentStamina > 10f;

        float speed = canRun ? runSpeed : walkSpeed;

        if (canRun)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else
        {
            currentStamina += staminaRegen * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * speed * Time.deltaTime);

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
}