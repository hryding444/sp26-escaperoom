using UnityEngine;

public class DoorUnlockController : MonoBehaviour
{
    public SocketCheck[] requiredSockets;
    public OpenMiniDoor door;   // existing door script

    void Update()
    {
        if (AllSocketsFilled())
        {
            door.UnlockDoor();
            enabled = false; // stop checking
        }
    }

    bool AllSocketsFilled()
    {
        foreach (var socket in requiredSockets)
        {
            if (!socket.IsFilled)
                return false;
        }
        return true;
    }
}
