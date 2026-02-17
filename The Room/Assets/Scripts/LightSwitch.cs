using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light light_src;
    public InputActionReference action;
    private bool switchOn = false;

    void Start()
    {
        light_src = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (!switchOn)
            {
                light_src.color = Color.red;
            } else
            {
                light_src.color = Color.white;
            }
            switchOn = !switchOn;
        };
    }

}
