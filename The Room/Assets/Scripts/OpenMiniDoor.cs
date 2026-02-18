using UnityEngine;
using UnityEngine.InputSystem;

public class OpenMiniDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform rightDoor;   // Assign the right door child here

    [Header("Input")]
    public InputActionReference interactAction;

    [Header("Settings")]
    public float openAngle = 90f;    // Rotation angle in degrees
    public float openSpeed = 2f;     // How fast the door rotates

    private bool playerInRange = false;
    private bool isOpen = false;
    private bool isLocked = true;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Cache initial rotation
        closedRotation = rightDoor.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    // Called when player presses the assigned input action
    void OnInteract(InputAction.CallbackContext ctx)
    {
        // Only respond if player is close, door not open, and unlocked
        if (!playerInRange || isOpen || isLocked)
            return;

        isOpen = true;
    }

    void Update()
    {
        // Animate door opening smoothly
        if (isOpen)
        {
            rightDoor.localRotation = Quaternion.Lerp(
                rightDoor.localRotation,
                openRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    // Called by your socket manager when all sockets are filled
    public void UnlockDoor()
    {
        isLocked = false;
    }

    // Detect player entering the proximity trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
