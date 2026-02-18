using UnityEngine;


public class SocketCheck : MonoBehaviour
{
    public bool IsFilled => socket.hasSelection;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }
}