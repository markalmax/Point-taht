using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public float smoothness = 0.1f;
    public Vector2 offset;
    void FixedUpdate()
    {
        if(player == null) return;
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 smoothedPos = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), playerPos + offset, smoothness);
        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, transform.position.z);
    }
}
