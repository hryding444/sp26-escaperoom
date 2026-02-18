using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class GrowShrink : MonoBehaviour
{
    public InputActionReference action;
    public XROrigin XROrigin;
    public ContinuousMoveProvider dynamicMove;

    private Coroutine curr_coroutine;
    private bool growing = true;
    [SerializeField] private Vector3 bigScale;
    [SerializeField] private Vector3 smallScale;
    [SerializeField] private float duration = 1.0f;
    void Start() 
    {


        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (curr_coroutine != null)
            {
                StopCoroutine(curr_coroutine);
            }
            if (growing)
            {
                curr_coroutine = StartCoroutine(scaleSize(bigScale, duration));
                growing = false;
                dynamicMove.moveSpeed = 10f;
            } else
            {
                curr_coroutine = StartCoroutine(scaleSize(smallScale, duration));
                growing = true;
                dynamicMove.moveSpeed = 2.5f;
            }
        };
    }

    private void Update()
    {
        if (XROrigin.transform.localScale == bigScale)
        {
            dynamicMove.moveSpeed = 2.5f;
        }
        if (XROrigin.transform.localScale == smallScale)
        {
            dynamicMove.moveSpeed = 10f;
        }
    }

    private IEnumerator scaleSize(Vector3 finalScale,  float duration)
    {
        Vector3 startScale = XROrigin.transform.localScale;
        float currTime = 0f;

        while (currTime < duration)
        {
            float t = currTime / duration;
            XROrigin.transform.localScale = Vector3.Lerp(startScale, finalScale, t);
            currTime += Time.deltaTime;
            yield return null;
        }
        XROrigin.transform.localScale = finalScale;
    }
}
