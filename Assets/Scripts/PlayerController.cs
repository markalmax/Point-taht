using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool canMove = false;
    public float moveSpeed = 10f;
    public float moveMult = 1.5f;
    public float moveVelocity;
    public float maxVelocity = 10f;
    public float reelSpeedMult = 0.1f;
    public float maxGrappleDistance = 20f;
    public PhysicsMaterial2D ball;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;
    private GameManager gm;
    private effects ef;
    private Collider2D col;
    private Trophy trophy;
    private float moveInput;
    private Vector2 mospos;
    private bool isGrappling;
    void Start()
    {
        ef = GetComponent<effects>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        gm = FindFirstObjectByType<GameManager>();
        trophy = FindFirstObjectByType<Trophy>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        moveVelocity = moveSpeed;
    }      
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        gm = FindFirstObjectByType<GameManager>();
        trophy = FindFirstObjectByType<Trophy>();
        //if(gm==null){canMove=false;Release();}
    }
    void Update()
    {
        
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
                ef.grappleSound();
                dj.distance = Vector2.Distance(transform.position, hit.point);
                lr.enabled = true;
                lr.positionCount = 2;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, hit.point);
            }
            if (hit.collider == null)
            {
                Release();
            }
        }
        if (Input.GetMouseButtonUp(0)&&canMove)
        {
            Release();
        }
       
    }
    public void Release()
    {
        dj.enabled = false;
        lr.enabled = false;
        lr.positionCount = 0;
        isGrappling = false;
    }   
    void FixedUpdate()
    {
        if(canMove)rb.AddForce(new Vector2(moveInput, 0) * moveVelocity);
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
        
        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);
            //if(!isGrounded())dj.distance -= moveInput.y * reelSpeedMult;
            if (isGrounded() && moveInput != 0) dj.distance -= 1f * Time.deltaTime;
        }
        if (!isGrappling && isGrounded() && canMove)
        {
            col.sharedMaterial = ball;
            moveVelocity = moveSpeed*moveMult;
        }
        else
        {
            col.sharedMaterial = null;
            moveVelocity = moveSpeed;
        } 
    }
    public bool isGrounded() { return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground")).collider != null; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lose"))trophy.Lose();
        if (collision.gameObject.CompareTag("Win"))trophy.Win();
    }
    public void SpawnTP(Vector3 position)
    {
        transform.position = position;
    }
}