using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left = -1, right = 1
    }

    [SerializeField] private Rigidbody2D body2D;

    [Header("Walk Properties")]
    public float maxSpeed = 5f;
    public float accelerationTime = 0.5f;
    public float decelerationTime = 0.25f;

    [Header("Jump Properties")]
    public float apexHeight = 3.5f;
    public float apexTime = 0.5f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.55f;
    public Vector2 groundCheckSize = new(0.75f, .2f);

    private Vector2 velocity;
    private float acceleration;
    private float deceleration;

    private float gravity;
    private float jumpVel;

    private Vector2 playerInput;
    private bool jumpPressed = false;

    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        body2D.gravityScale = 0;
    }

    void Update()
    {
        playerInput = new()
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetButtonDown("Jump") ? 1 : 0
        };

        if (playerInput.y == 1) jumpPressed = true;
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        ProcessWalkInput();
        ProcessJumpInput();

        body2D.linearVelocity = velocity;
    }

    private void ProcessWalkInput()
    {
        if (playerInput.x != 0)
        {
            if (Mathf.Sign(playerInput.x) != Mathf.Sign(velocity.x)) velocity.x *= -1;
            velocity.x += playerInput.x * acceleration * Time.fixedDeltaTime;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
        else if (Mathf.Abs(velocity.x) > 0.005f)
        {
            velocity.x += -Mathf.Sign(velocity.x) * deceleration * Time.fixedDeltaTime;
        }
        else
        {
            velocity.x = 0;
        }
    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
