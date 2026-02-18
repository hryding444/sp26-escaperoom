using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class OpenMiniDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform rightDoor;          // The part that rotates
    public Renderer doorRenderer;        // Renderer for the door material

    [Header("Input")]
    public InputActionReference interactAction;

    [Header("Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("UI")]
    public Canvas popupCanvas;           
    public TextMeshProUGUI popupText;

    [Header("Visual Feedback")]
    public Color lockedEmission = Color.red; // Red glow
    public float tintDuration = 0.5f;       // Time to show the red emission

    private bool playerInRange = false;
    private bool isOpen = false;
    private bool isLocked = true;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Color originalEmission;

    void Start()
    {
        closedRotation = rightDoor.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        if (popupCanvas != null)
            popupCanvas.gameObject.SetActive(false);

        if (doorRenderer != null)
        {
            // Enable emission keyword and store original emission
            doorRenderer.material.EnableKeyword("_EMISSION");
            originalEmission = doorRenderer.material.GetColor("_EmissionColor");
        }
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

    void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!playerInRange || isOpen)
            return;

        if (isLocked)
        {
            // Show popup text
            if (popupCanvas != null)
                StartCoroutine(ShowPopup(10f));

            // Flash red emission
            if (doorRenderer != null)
                StartCoroutine(FlashRedEmission());

            return;
        }

        isOpen = true;
    }

    void Update()
    {
        if (isOpen)
        {
            rightDoor.localRotation = Quaternion.Lerp(
                rightDoor.localRotation,
                openRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

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

    // Show popup text for a fixed duration
    IEnumerator ShowPopup(float duration)
    {
        popupCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        popupCanvas.gameObject.SetActive(false);
    }

    // Flash emission red and then fade back
    IEnumerator FlashRedEmission()
    {
        float elapsed = 0f;
        Material mat = doorRenderer.material;

        while (elapsed < tintDuration)
        {
            float t = elapsed / tintDuration;
            // Lerp emission color from original to red
            mat.SetColor("_EmissionColor", Color.Lerp(originalEmission, lockedEmission, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.SetColor("_EmissionColor", originalEmission); // restore original
    }
}