using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController pc;
    private Vector2 contactSquash;
    private Vector2 lastFrameVelocity;
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
        finalX *= contactSquash.x;
        finalY *= contactSquash.y;
        contactSquash = Vector2.Lerp(contactSquash, Vector2.one, 0.15f);
        transform.localScale = new Vector3(finalX, finalY, 1f);
        //Debug.Log("HH: " + hh + " HV: " + hv);
        lastFrameVelocity = rb.linearVelocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CV: " + lastFrameVelocity+" LV: " + rb.linearVelocity);
        if(rb.linearVelocityX == 0 || rb.linearVelocityY == 0)
        {
            float collisionStrengthX = Mathf.Clamp01(Mathf.Abs(lastFrameVelocity.x) / pc.maxVelocity);
            float collisionStrengthY = Mathf.Clamp01(Mathf.Abs(lastFrameVelocity.y) / pc.maxVelocity);
            float squashX = 1f - (collisionStrengthY * 0.5f);
            float stretchY = 1f + (collisionStrengthX * 0.5f);
            contactSquash = new Vector2(squashX, stretchY);
        }
    }
}
