using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left = -1, right = 1
    }

    [SerializeField] private Rigidbody2D body2D;

    [Header("WalkProperties")]
    public float maxSpeed = 5f;
    public float accelerationTime = 0.5f;
    public float deccelerationTime = 0.25f;

    [Header("Jump Properties")]
    public float apexHeight = 3.5f;
    public float apexTime = 0.5f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.55f;
    public Vector2 groundCheckSize = new(0.75f, .2f);

    private Vector2 velocity;
    private float acceleration;
    private float decceleration;

    private float gravity;
    private float jumpVel;

    private Vector2 playerInput;
    private bool jumpPressed = false;

    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        decceleration = maxSpeed / deccelerationTime;

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
        MovementUpdate():
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        ProcessWalkInput();
        ProcessJumpInput();

        body2D.linearVelocity = velocity;
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
