using UnityEngine;
using TMPro;

public class SocketManager : MonoBehaviour
{
    [Header("Sockets")]
    public SocketCheck[] sockets;  // All socket objects in your puzzle

    [Header("UI")]
    public TextMeshProUGUI progressText;  // Assign your world-space TMP text here

    [Header("Door")]
    public OpenMiniDoor door;   // Assign your DoorInteract script here

    // Optional: colors for progress text
    public Color emptyColor = Color.red;
    public Color filledColor = Color.green;

    private int lastFilledCount = -1; // To only update when progress changes

    void Update()
    {
        int filledCount = 0;

        foreach (var socket in sockets)
        {
            if (socket.IsFilled) filledCount++;
        }

        // Only update UI if progress changed
        if (filledCount != lastFilledCount)
        {
            progressText.text = $"Progress: {filledCount} / {sockets.Length}";

            // Change color based on progress
            progressText.color = (filledCount == sockets.Length) ? filledColor : emptyColor;

            lastFilledCount = filledCount;
        }

        // Unlock the door when all sockets filled
        if (filledCount == sockets.Length)
        {
            door.UnlockDoor();
        }
    }
}
