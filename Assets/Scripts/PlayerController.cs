using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool canMove = false;
    public float moveSpeed = 10f;
    public float maxVelocity = 10f;
    public float reelSpeedMult = 0.1f;
    public float maxGrappleDistance = 20f;
    public PhysicsMaterial2D ball;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;
    private GameManager gm;
    private MenuController mc;
    private Collider2D col;
    private float moveInput;
    private Vector2 mospos;
    private bool isGrappling;    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        gm = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gm != null)
        {
            if (gm.flag) canMove = true;
        }
        else canMove = true;
        moveInput = Input.GetAxis("Horizontal");
        mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)&&canMove)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mospos - (Vector2)transform.position, maxGrappleDistance, LayerMask.GetMask("Ground"));
            if (hit)
            {
                isGrappling = true;
                dj.enabled = true;
                dj.connectedAnchor = hit.point;
                dj.distance = Vector2.Distance(transform.position, hit.point);
                lr.enabled = true;
                lr.positionCount = 2;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, hit.point);
            }
        }
        if (Input.GetMouseButtonUp(0)&&canMove)
        {
            dj.enabled = false;
            lr.enabled = false;
            lr.positionCount = 0;
            isGrappling = false;
        }
    }   
    void FixedUpdate()
    {
        if(canMove)rb.AddForce(new Vector2(moveInput, 0) * moveSpeed);
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
        
        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);
            //if(!isGrounded())dj.distance -= moveInput.y * reelSpeedMult; (reeling)
            if (isGrounded() && moveInput != 0) dj.distance -= 1f * Time.deltaTime;
        }
        if (!isGrappling && isGrounded() && canMove)
        {
            col.sharedMaterial = ball;
        }
        else col.sharedMaterial = null;
    }
    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lose"))lose();
        if (collision.gameObject.CompareTag("Win"))win();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Play")) ;
        if (collision.gameObject.CompareTag("Levels")) ;
        if (collision.gameObject.CompareTag("Settings")) ;
        if (collision.gameObject.CompareTag("Back")) ;  
        if (collision.gameObject.CompareTag("Quit")) ; 
        
    }
    public void win()
    {
        PlayerPrefs.SetFloat("HighScore", math.min(gm.timer, PlayerPrefs.GetFloat("HighScore",Mathf.Infinity)));
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
    public void lose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}