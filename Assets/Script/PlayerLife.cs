using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : CharacterLife
{
    private new const int MAX_HEALTH = 3;
    private const string IS_INVINCIBLE_PARAM = "IsInvincible";
    
    private bool m_IsInvincible = false;

    [SerializeField] private float m_InvincibilityDelay = 1f;
    [SerializeField] private float m_InvincibilityAnimationDelay = 0.8f;

    private CapsuleCollider2D m_PlayerCollider;
    private Rigidbody2D m_Rigidbody;

    [SerializeField] private Image[] m_Hearts;
    [SerializeField] private Sprite m_FullHeart;
    [SerializeField] private Sprite m_EmptyHeart;

    /// <summary>
    /// Gets the singleton instance of the PlayerLife.
    /// </summary>
    #region Singleton
    public static PlayerLife Instance;

    protected override void Awake()
    {
        base.Awake();
        // Check if there is more than one instance in the scene
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerLife in the scene");
            return;
        }

        Instance = this;

    #endregion
    }

    protected override void Start()
    {
        base.Start();

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Update the hearts UI based on player health
        foreach (Image img in m_Hearts)
        {
            img.sprite = m_EmptyHeart;
        }

        for (int i = 0; i < m_Health; i++)
        {
            m_Hearts[i].sprite = m_FullHeart;
        }
    }

    /// <summary>
    /// Deals damage to the player.
    /// </summary>
    public override void TakeDamage()
    {
        if (!m_IsInvincible)
        {
            base.TakeDamage();

            // Disable player attack while player's taking damage
            PlayerAttack.Instance.enabled = false;

            // Make the player temporarily invincible
            m_IsInvincible = true;
            StartCoroutine(InvincibilityFlash(m_InvincibilityAnimationDelay));
            StartCoroutine(HandleInvincibilityDelay(m_InvincibilityDelay));
        }
    }

    /// <summary>
    /// Handles the death of the player.
    /// </summary>
    public override void Die()
    {
        // Disable player movement
        PlayerMovement.Instance.enabled = false;

        // Disable player attack
        PlayerAttack.Instance.enabled = false;

        // Trigger death animation
        TriggerDeathAnimation("Die");

        // Disable interaction with the environment
        DisableCollider(m_PlayerCollider);
        m_Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        m_Rigidbody.velocity = Vector3.zero;

        // Call Game Over function
        GameOverManager.Instance.OnPlayerDeath();
    }

    /// <summary>
    /// Respawns the player.
    /// </summary>
    public void Respawn()
    {
        // Re-enable player movement
        PlayerMovement.Instance.enabled = true;

        // Trigger respawn animation
        m_Animator.SetTrigger("Respawn");

        // Re-enable interaction with the environment
        m_Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        m_PlayerCollider.enabled = true;

        // Reset player health
        m_Health = MAX_HEALTH;
    }

    /// <summary>
    /// Flashes the player to indicate invincibility.
    /// </summary>
    public IEnumerator InvincibilityFlash(float delay)
    {
        if (m_IsInvincible)
        {
            if (m_Animator != null)
            {
                m_Animator.SetBool(IS_INVINCIBLE_PARAM, true);
                yield return new WaitForSeconds(delay);
                m_Animator.SetBool(IS_INVINCIBLE_PARAM, false);
            }
        }
    }

    /// <summary>
    /// Handles the delay for player invincibility.
    /// </summary>
    public IEnumerator HandleInvincibilityDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_IsInvincible = false;
    }

    
}
