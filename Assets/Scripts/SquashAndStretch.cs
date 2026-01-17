using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController pc;
    private Vector2 contactSquash;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }
    void FixedUpdate()
    {
        float hv = rb.linearVelocityY;
        float hh = rb.linearVelocityX;
        float tx = Mathf.Clamp01(Mathf.Abs(hh) / pc.maxVelocity);
        float ty = Mathf.Clamp01(Mathf.Abs(hv) / pc.maxVelocity);
        float finalX = 1f + tx - (0.5f * ty);
        float finalY = 1f + ty - (0.5f * tx);
        transform.localScale = new Vector3(finalX, finalY, 1f);
        Debug.Log("HH: " + hh + " HV: " + hv);
    }
    
}
