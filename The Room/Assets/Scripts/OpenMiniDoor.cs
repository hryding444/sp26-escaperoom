using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class OpenMiniDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform rightDoor;
    public Renderer doorRenderer;

    [Header("Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("UI")]
    public Canvas popupCanvas;
    public TextMeshProUGUI popupText;

    [Header("Visual Feedback")]
    public Color lockedEmission = Color.red;
    public float tintDuration = 0.5f;

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
            doorRenderer.material.EnableKeyword("_EMISSION");
            originalEmission = doorRenderer.material.GetColor("_EmissionColor");
        }
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

    // Trigger events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isLocked)
        {
            if (popupCanvas != null)
                popupCanvas.gameObject.SetActive(true);

            if (doorRenderer != null)
                StartCoroutine(FlashRedEmission());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (popupCanvas != null)
                popupCanvas.gameObject.SetActive(false);
        }
    }

    // Flash emission red and fade back
    IEnumerator FlashRedEmission()
    {
        float elapsed = 0f;
        Material mat = doorRenderer.material;

        while (elapsed < tintDuration)
        {
            float t = elapsed / tintDuration;
            mat.SetColor("_EmissionColor", Color.Lerp(originalEmission, lockedEmission, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.SetColor("_EmissionColor", originalEmission);
    }
}