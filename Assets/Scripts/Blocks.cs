using UnityEngine;

public class Blocks : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Mathf.Abs(collision.relativeVelocity.magnitude) > 10f)
            {
                Destroy(gameObject,1);
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<ParticleSystem>().Play();
            }
        } 
    }
}
