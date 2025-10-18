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
        
    }
    void OnDisable()
    {
        moveAction.Disable();
        grapple.Disable();
    }
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        grappleInput = grapple.ReadValue<float>() > 0.5f;
        mospos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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
        if (grappleInput)
        {
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
        else
        {
            dj.enabled = false;
            lr.enabled = false;
            lr.positionCount = 0;
        }
    }
}