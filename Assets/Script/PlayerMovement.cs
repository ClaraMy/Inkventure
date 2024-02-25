using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float GRAVITY_VALUE = -19.81f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private Vector2 m_MoveVector;
    private float m_JumpVelocity;

    [SerializeField] private float m_Speed = 7.0f;  
    [SerializeField] private float m_JumpHeight = 7.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;
     
    [SerializeField] private Transform groundCheck;
    public float groundCheckRadius;
    private float horizontalInput;
    public LayerMask collisionLayers;

    private bool isGrounded;

    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip(horizontalInput);

        float characterVelocity = Mathf.Abs(horizontalInput);
        animator.SetFloat("Speed", characterVelocity);
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        Move(m_MoveVector);
        ApplyGravity();
    }

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        m_MoveVector = context.ReadValue<Vector2>();
        horizontalInput = m_MoveVector.x;
    }

    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }

    public void Move(Vector2 m_MoveVector)
    {
        Vector3 targetVelocity = new Vector2(m_MoveVector.x * m_Speed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, m_TurnSmoothTime);
    }

    private void ApplyGravity()
    {
        if (isGrounded)
        {
            if (m_JumpVelocity < 0)
            {
                m_JumpVelocity = 0f;
            }
        }
        else
        {
            m_JumpVelocity += GRAVITY_VALUE * Time.deltaTime;
        }

        transform.Translate(Vector3.up * m_JumpVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        m_JumpVelocity += Mathf.Sqrt(m_JumpHeight * -GRAVITY_VALUE);
        //rb.velocity = new Vector2(rb.velocity.x, m_JumpHeight);
    }

    void Flip(float _horizontalInput)
    {
        if(_horizontalInput> 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_horizontalInput < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
