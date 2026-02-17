using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject rotation_center;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotation_center.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
