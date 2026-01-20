using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
