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

    public float terminalSpeed;

    public float fallingDetector = 4f;

    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVel = 2 * apexHeight / apexTime;

        body2D.gravityScale = -gravity * terminalSpeed;
    }

    void Update()
    {
        playerInput = new()
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetButtonDown("Jump") ? 1 : 0
        };

        if (playerInput.y == 1) jumpPressed = true;
        if (IsFalling()) jumpPressed = false;
        Debug.Log("a"+ IsGrounded());
        Debug.Log("b"+ jumpPressed);
        Debug.Log("c" + velocity.y);
        Debug.Log("d" + body2D.position.y);
        Debug.Log("e" + IsFalling());

        if (Input.GetKey(KeyCode.J))
        {
            fallingDetector = 8;
        }
        else
        {
            fallingDetector = 4;
        }

        if (Input.GetKey(KeyCode.V))
        {
            maxSpeed = 10;
        }
        else
        {
            maxSpeed = 5;
        }
     

    }

    private void FixedUpdate()
    {
        MovementUpdate();

    }

    private void MovementUpdate()
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

    private void ProcessJumpInput()
    {
        
            if (jumpPressed)
            {
                body2D.gravityScale = -jumpVel;
            }   

        

        else
        {
            body2D.gravityScale = -gravity * terminalSpeed;
        }
    }

    public bool IsWalking()
    {
        if (playerInput.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(body2D.position, groundCheckSize, 0f, Vector2.down, groundCheckDistance, groundLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
        
    }

    public bool IsFalling()
    {
        if (Physics2D.BoxCast(body2D.position, groundCheckSize, 0f, Vector2.down, fallingDetector, groundLayer))
        {
            return false;
        }

        else
        {
            return true;
        }
    }


    public FacingDirection GetFacingDirection()
    {

        if (playerInput.x>=0)
        {
            return FacingDirection.right;
        }
        else
        {
            return FacingDirection.left;
        }

       
    }
}
