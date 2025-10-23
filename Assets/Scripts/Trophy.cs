using UnityEngine;

public class Trophy : MonoBehaviour
{
    public float oscillationHeight = 0.5f;
    public float oscillationSpeed = 2f;
    public float rotationSpeed = 1f;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed, 0);        
        float newY = startPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
