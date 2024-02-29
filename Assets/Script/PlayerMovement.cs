using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float GRAVITY_VALUE = -19.81f;

    private float m_JumpVelocity;
    private float m_HorizontalInput;

    [SerializeField] private float m_Speed = 7.0f;
    [SerializeField] private float m_JumpHeight = 7.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
    [SerializeField] private float m_GroundCheckRadius = 0.5f;

    private bool m_IsGrounded;

    private Vector3 m_Velocity = Vector3.zero;
    private Vector2 m_MoveVector;

    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_CollisionLayers = default;

    /// <summary>
    /// Gets the singleton instance of the PlayerMovement.
    /// </summary>
    #region Singleton
    public static PlayerMovement Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerMovement in the scene");
            return;
        }

        Instance = this;
    }
    #endregion
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // Flip the player sprite based on input
        Flip(m_HorizontalInput);

        // Set animator parameter for character speed
        float characterVelocity = Mathf.Abs(m_HorizontalInput);
        m_Animator.SetFloat("Speed", characterVelocity);
    }
    private void FixedUpdate()
    {
        // Check if the player is grounded
        m_IsGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, m_GroundCheckRadius, m_CollisionLayers);

        // Move the player and apply gravity
        Move(m_MoveVector);
        ApplyGravity();
    }

    /// <summary>
    /// Reads the move input from the player.
    /// </summary>
    /// <param name="context">The input context from the Input System.</param>
    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
        m_HorizontalInput = m_MoveVector.x;
    }

    /// <summary>
    /// Reads the jump input from the player.
    /// </summary>
    /// <param name="context">The input context from the Input System.</param>
    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_IsGrounded)
            {
                // Jump only if the player is grounded
                Jump();
            }
        }
    }

    /// <summary>
    /// Moves the player based on the input vector.
    /// </summary>
    /// <param name="m_MoveVector">The movement vector input.</param>
    public void Move(Vector2 m_MoveVector)
    {
        Vector3 targetVelocity = new Vector2(m_MoveVector.x * m_Speed, m_Rigidbody.velocity.y);
        m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_TurnSmoothTime);
    }

    /// <summary>
    /// Applies gravity to the player.
    /// </summary>
    private void ApplyGravity()
    {
        if (m_IsGrounded)
        {
            if (m_JumpVelocity < 0)
            {
                // Reset gravity value if the player is grounded
                m_JumpVelocity = 0f;
            }
        }
        else
        {
            // Apply gravity if the player is not grounded
            m_JumpVelocity += GRAVITY_VALUE * Time.deltaTime;
        }

        // Apply vertical movement due to gravity
        transform.Translate(Vector3.up * m_JumpVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Makes the player jump.
    /// </summary>
    private void Jump()
    {
        // Calculate and apply jump velocity
        m_JumpVelocity += Mathf.Sqrt(m_JumpHeight * -GRAVITY_VALUE);
    }

    /// <summary>
    /// Flips the player sprite based on the horizontal input.
    /// </summary>
    /// <param name="m_HorizontalInput">The horizontal input value.</param>
    void Flip(float m_HorizontalInput)
    {
        if(m_HorizontalInput> 0.1f)
        {
            // Flip the player sprite to face right
            m_SpriteRenderer.flipX = false;
        } else if (m_HorizontalInput < -0.1f)
        {
            // Flip the player sprite to face left
            m_SpriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Draws the ground check radius in the Unity editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundCheckRadius);
    }
}
