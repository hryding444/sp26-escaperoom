using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light homeLight;
    public Light kitchenLight;
    public AudioSource startAudio;
    void Start()
    {
        homeLight.intensity = 1f;
        kitchenLight.intensity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startEscape()
    {
        StartCoroutine(turnLight(homeLight, 0f, 1f));
        StartCoroutine(turnLight(kitchenLight, 50f, 4f));
        startAudio.Play();
    }

    
    private IEnumerator turnLight(Light light, float end_intensity, float duration)
    {
        float curr_intensity = light.intensity;
        float curr_time = 0f;


        while (curr_time < duration)
        {
            float t = end_intensity / duration;
            light.intensity = Mathf.Lerp(curr_intensity, end_intensity, t);
            curr_time += Time.deltaTime;
            yield return null;

        }
        light.intensity = end_intensity;
    }
}
