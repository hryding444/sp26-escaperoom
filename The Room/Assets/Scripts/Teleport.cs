using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionReference action;
    public GameObject teleportPad;
    private bool inRoom = true;
    void Start()
    {
        Vector3 roomPos = transform.position;
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (inRoom) {
                transform.position = teleportPad.transform.position + (Vector3.up * 5.0f);
            } else
            {
                transform.position = roomPos;
            }
            inRoom = !inRoom;
        };
    }

    
}
