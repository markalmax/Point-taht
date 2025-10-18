using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 10f;
    public float maxVelocity = 10f;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private DistanceJoint2D dj;
    private Vector2 moveInput;
    private bool grappleInput;
    private Vector2 mospos;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction grapple;
    
    // cached subscription delegates so we can unsubscribe cleanly
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
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
    }
    void OnEnable()
    {
        moveAction.Enable();
        grapple.Enable();
        // set up callbacks
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
        // perform grapple (same logic that ran when input was true)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mospos - (Vector2)transform.position, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hit)
        {
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
    }
    void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            rb.AddForce(moveInput.normalized * moveSpeed);
            if (rb.linearVelocity.magnitude > maxVelocity)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
            }
        }
    }
}