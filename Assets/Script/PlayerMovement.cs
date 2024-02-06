using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float m_Speed = 5.0f;  
    [SerializeField] private float m_JumpHeight = 1.0f;
    [SerializeField] private float m_TurnSmoothTime = 0.1f;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        float horizontalMovement = Input.GetAxisRaw("Horizontal") * m_Speed * Time.deltaTime;

        Move(horizontalMovement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void Move(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, m_TurnSmoothTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, m_JumpHeight * 10));
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
