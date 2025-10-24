using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public float cameraSize = 10f;
    public float smoothness = 0.1f;
    public float shakeIntensityMultiplier = 0.1f;
    public float maxShakeIntensity = 0.5f;
    public float shakeFrequency = 25f;
    public float shakeDampingSpeed = 3f;
    private float currentShakeIntensity = 0f;
    public Vector2 offset;
    private Vector3 originalPosition;
    private Rigidbody2D rb;
    private Camera cam;
    private float shakeTime;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;
        float cameraSizeSmoothness = cam.orthographicSize;
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, cameraSize, cameraSizeSmoothness * Time.fixedDeltaTime);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 smoothedPos = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), playerPos + offset, smoothness);
        originalPosition = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);

        float speedPercent = rb.linearVelocity.magnitude / rb.GetComponent<PlayerController>().maxVelocity;
        float targetShakeIntensity = speedPercent * shakeIntensityMultiplier;
        targetShakeIntensity = Mathf.Min(targetShakeIntensity, maxShakeIntensity);
        currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, targetShakeIntensity, Time.fixedDeltaTime * shakeDampingSpeed);

        if (currentShakeIntensity > 0)
        {
            shakeTime += Time.fixedDeltaTime * shakeFrequency;
            Vector3 shakeOffset = new Vector3(Mathf.PerlinNoise(shakeTime,0)*2-1,Mathf.PerlinNoise(0,shakeTime)*2-1,0) * currentShakeIntensity;
            transform.position = originalPosition + shakeOffset;
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
