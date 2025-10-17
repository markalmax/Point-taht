using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 400f, maxSpeed = 5f, maxGrappleDistance = 8f, reelSpeed = 3f;
    public LayerMask grappleMask = -1;
    Rigidbody2D rb;
    LineRenderer lr;
    DistanceJoint2D dj;
    PlayerInput pi; 
    InputAction moveA, attackA, lookA;
    Vector2 move;
    Vector2 grapplePoint;
    bool grappling; 
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        pi = GetComponent<PlayerInput>();

        moveA = pi.actions["Move"];
        attackA = pi.actions["Attack"];
        lookA = pi.actions["Look"];

        lr.enabled = false;
        dj.enabled = false; 
    }

    void OnEnable() { moveA.Enable(); attackA.Enable(); lookA.Enable(); }
    void OnDisable() { moveA.Disable(); attackA.Disable(); lookA.Disable(); }

    void Update()
    {
        float moveX = moveA.ReadValue<Vector2>().x;
        move = new Vector2(moveX, 0);
        bool attack = attackA.ReadValue<float>() > 0.5f;
        if (attack)
        {
            var origin = (Vector2)transform.position;
            Vector2 aim;
            if (Mouse.current != null)
                aim = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            else
            {
                var stick = lookA.ReadValue<Vector2>();
                aim = stick.sqrMagnitude > 0.01f ? origin + stick.normalized * maxGrappleDistance : origin + Vector2.right * maxGrappleDistance;
            }
            var dir = (aim - origin).normalized;
            var hit = Physics2D.Raycast(origin, dir, maxGrappleDistance, grappleMask);
            if (hit)
            {
                print("Grappled to " + hit.point);
                grapplePoint = hit.point;
                grappling = true;
                lr.enabled = true;
                dj.enabled = true;
                dj.connectedAnchor = grapplePoint;
                dj.distance = Vector2.Distance(origin, grapplePoint);
            }
        }
        if (!attack) { grappling = false; lr.enabled = false; dj.enabled = false; }

    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(move.x, 0) * moveForce * Time.fixedDeltaTime);
        if (rb.linearVelocity.magnitude > maxSpeed) rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        if (grappling)
        {
            lr.SetPosition(0, transform.position); lr.SetPosition(1, grapplePoint);
            if (dj.distance > 0.5f) dj.distance = Mathf.Max(0.5f, dj.distance - reelSpeed * Time.fixedDeltaTime);
        }
    }
}