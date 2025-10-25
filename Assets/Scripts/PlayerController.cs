using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 10f;
    public float maxVelocity = 10f;
    public float reelSpeedMult = 0.1f;
    public float maxGrappleDistance = 20f;
    public PhysicsMaterial2D ball;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;
    private GameManager gm;
    private Collider2D col;
    private Vector2 moveInput;
    private bool grappleInput;
    private Vector2 mospos;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction grapple;
    private bool isGrappling;    
    private System.Action<InputAction.CallbackContext> onGrapplePerformed;
    private System.Action<InputAction.CallbackContext> onGrappleCanceled;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }
        moveAction = playerInput.actions["Move"];
        grapple = playerInput.actions["Attack"];
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        gm = FindFirstObjectByType<GameManager>();
    }
    void OnEnable()
    {
        moveAction.Enable();
        grapple.Enable();
        onGrapplePerformed = ctx => OnGrapplePressed(ctx);
        onGrappleCanceled = ctx => OnGrappleReleased(ctx);
        grapple.performed += onGrapplePerformed;
        grapple.canceled += onGrappleCanceled;
        
    }
    void OnDisable()
    {
        moveAction.Disable();
        grapple.performed -= onGrapplePerformed;
        grapple.canceled -= onGrappleCanceled;
        grapple.Disable();
    }
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        mospos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }   
    
    private void OnGrapplePressed(InputAction.CallbackContext ctx)
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

    private void OnGrappleReleased(InputAction.CallbackContext ctx)
    {
        dj.enabled = false;
        lr.enabled = false;
        lr.positionCount = 0;
        isGrappling = false;
    }
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(moveInput.x, 0) * moveSpeed);
        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }
        
        if (isGrappling)
        {
            lr.SetPosition(0, transform.position);
            //if(!isGrounded())dj.distance -= moveInput.y * reelSpeedMult; (reeling)
            if (isGrounded() && moveInput.x != 0) dj.distance -= 1f * Time.deltaTime;
        }
        if (!isGrappling && isGrounded())
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
    public void win()
    {
        PlayerPrefs.SetFloat("HighScore", math.min(gm.timer, PlayerPrefs.GetFloat("HighScore",Mathf.Infinity)));
        Debug.Log(gm.timer);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
    public void lose()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}