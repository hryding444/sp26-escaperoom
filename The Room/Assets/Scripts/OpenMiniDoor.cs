using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;

public class OpenMiniDoor : MonoBehaviour
{
    [Header("Door Parts")]
    public Transform rightDoor;
    public Transform leftDoor;
    public Renderer doorRenderer;

    [Header("Settings")]
    public float openAngleRight = 90f;
    public float openAngleLeft = 90f;
    public float openSpeed = 2f;

    [Header("UI")]
    public Canvas popupCanvas;
    public TextMeshProUGUI popupText;

    [Header("Visual Feedback")]
    public Color lockedEmission = Color.red;
    public float tintDuration = 0.5f;

    public AudioSource fanfare;
    public AudioSource background;

    private bool isOpen = false;
    private bool isLocked = true;
    private Quaternion closedRotation;
    private Quaternion openRotationRight;
    private Quaternion openRotationLeft;
    private Color originalEmission;

    public GameManager manager;

    void Start()
    {
        closedRotation = rightDoor.localRotation;
        openRotationRight = closedRotation * Quaternion.Euler(0f, openAngleRight, 0f);
        openRotationLeft = closedRotation * Quaternion.Euler(0f, openAngleLeft, 0f);

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
        
    }

    public void UnlockDoor()
    {
        fanfare.Play();
        background.Stop();
        isLocked = false;
        rightDoor.localRotation = Quaternion.Lerp(
                rightDoor.localRotation,
                openRotationRight,
                Time.deltaTime * openSpeed
            );

        leftDoor.localRotation = Quaternion.Lerp(
            rightDoor.localRotation,
            openRotationLeft,
            Time.deltaTime * openSpeed
        );
    }

    // Trigger events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isLocked)
        {
            // Show the popup
            if (popupCanvas != null)
                //StartCoroutine(ShowPopupForSeconds(5f));
                popupCanvas.gameObject.SetActive(true);

            // Flash red emission
            if (doorRenderer != null)
                StartCoroutine(FlashRedEmission());

            manager.startEscape();
        }

        else
        {
            // Door is unlocked → open it
            isOpen = true;
        }
    }

    // Coroutine to show popup for a fixed duration
    private IEnumerator ShowPopupForSeconds(float duration)
    {
        popupCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        popupCanvas.gameObject.SetActive(false);
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