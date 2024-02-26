using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private float timer = 0f;

    [SerializeField] private float timeToAttack = 0.25f;

    private Animator animator;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private GameObject RightAttackArea = default;
    [SerializeField] private GameObject LeftAttackArea = default;

    void Start()
    {
        animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If the player is currently attacking, keep track of the time elapsed since the start of the attack
        if (isAttacking)
        {
            timer += Time.deltaTime;

            // If the time to attack has elapsed, reset the attack parameters
            if (timer >= timeToAttack)
            {
                timer = 0;
                isAttacking = false;

                // Deactivate attack colliders and update animator parameter
                RightAttackArea.SetActive(isAttacking);
                LeftAttackArea.SetActive(isAttacking);
                animator.SetBool("IsAttacking", isAttacking);
            }
        }
    }

    /// <summary>
    /// Reads attack input from the player.
    /// </summary>
    /// <param name="context">The input context from the Input System.</param>
    public void ReadAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    /// <summary>
    /// Makes the player attack.
    /// </summary>
    private void Attack()
    {
        isAttacking = true;

        // Update animator parameter to trigger attack animation
        animator.SetBool("IsAttacking", isAttacking);

        // Determine the direction of the attack based on the player's facing direction
        if (m_SpriteRenderer.flipX == false)
        {
            // Activate the appropriate attack collider based on the direction
            RightAttackArea.SetActive(isAttacking);
        }
        else
        {
            LeftAttackArea.SetActive(isAttacking);
        }
    }
}