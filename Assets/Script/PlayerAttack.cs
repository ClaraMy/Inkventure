using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    private bool m_IsAttacking = false;

    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private GameObject m_RightAttackArea = default;
    [SerializeField] private GameObject m_LeftAttackArea = default;

    /// <summary>
    /// Gets the singleton instance of the PlayerAttack.
    /// </summary>
    #region Singleton
    public static PlayerAttack Instance;

    private void Awake()
    {
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerAttack in the scene");
            return;
        }

        Instance = this;
    }
    #endregion

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
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
        m_IsAttacking = true;

        // Update animator parameter to trigger attack animation
        m_Animator.SetBool("IsAttacking", m_IsAttacking);

        // Determine the direction of the attack based on the player's facing direction
        if (m_SpriteRenderer.flipX == false)
        {
            // Activate the appropriate attack collider based on the direction
            m_RightAttackArea.SetActive(m_IsAttacking);
        }
        else
        {
            m_LeftAttackArea.SetActive(m_IsAttacking);
        }
    }

    /// <summary>
    /// Called by an animation event when the attack animation is finished.
    /// Resets the attack state and deactivates attack colliders.
    /// </summary>
    public void AttackAnimationEnd()
    {
        m_IsAttacking = false;

        // Deactivate attack colliders and update animator parameter
        m_RightAttackArea.SetActive(m_IsAttacking);
        m_LeftAttackArea.SetActive(m_IsAttacking);
        m_Animator.SetBool("IsAttacking", m_IsAttacking);
    }
}