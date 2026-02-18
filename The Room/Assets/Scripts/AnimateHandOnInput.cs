using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty triggerValue;
    public InputActionProperty gripValue;
    public Animator handAnim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float trigger = triggerValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        handAnim.SetFloat("Trigger", trigger);
        handAnim.SetFloat("Grip", grip);
    }
}
