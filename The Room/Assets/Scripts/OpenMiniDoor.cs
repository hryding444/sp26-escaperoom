using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class OpenMiniDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform rightDoor;   // Assign the right door child here

    [Header("Input")]
    public InputActionReference interactAction;

    [Header("Settings")]
    public float openAngle = 90f;    // Rotation angle in degrees
    public float openSpeed = 2f;     // How fast the door rotates

    [Header("UI")]
    public Canvas popupCanvas;            // The world-space canvas for the popup
    public TextMeshProUGUI popupText;     // The TMP text inside the popup

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

        if (popupCanvas != null)
            popupCanvas.gameObject.SetActive(false);
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
        if (!playerInRange || isOpen)
            return;
        
        if (isLocked)
        {
            // Show popup in front of player
            if (popupCanvas != null)
                StartCoroutine(ShowPopup(2f));
            return;
        }

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

    System.Collections.IEnumerator ShowPopup(float duration)
    {
        // Move canvas in front of player
        Camera cam = Camera.main;
        if (cam != null)
        {
            popupCanvas.transform.position = cam.transform.position + cam.transform.forward * 1.5f; // 1.5m in front
            popupCanvas.transform.rotation = Quaternion.LookRotation(cam.transform.forward); // face player
        }

        popupCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        popupCanvas.gameObject.SetActive(false);
    }
}
