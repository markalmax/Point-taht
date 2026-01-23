using UnityEngine;

public class Blocks : MonoBehaviour
{
    public float mult = 1.1f;
    public float strength = 20f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Mathf.Abs(collision.relativeVelocity.magnitude) > strength)
            {
                Destroy(gameObject,1);
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<ParticleSystem>().Play();
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = collision.relativeVelocity * mult;
            }
        } 
    }
}
