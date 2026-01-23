using UnityEngine;
using System.Collections;

public class SpriteControlller : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float threshold=1f; 
    public Sprite Smile;
    public Sprite Flat;
    public Sprite Frown;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (sr.sprite != Frown)
        {
            if (rb.linearVelocity.magnitude >= threshold)sr.sprite=Smile;
            else sr.sprite=Flat;
        }

        if (rb.linearVelocity.x < 0)
            sr.flipX = true;
        else if (rb.linearVelocity.x > 0)
            sr.flipX = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isGrounded())StartCoroutine(ShowSadSprite());
    }
    IEnumerator ShowSadSprite()
    {
        sr.sprite = Frown;
        yield return new WaitForSeconds(1f);
        sr.sprite = Flat;
    }
    public bool isGrounded() { return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")).collider != null; }
}
