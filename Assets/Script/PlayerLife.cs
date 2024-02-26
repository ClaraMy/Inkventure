using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private const int MAX_HEALTH = 3;
    private static int m_Health;

    private bool isInvincible = false;

    [SerializeField] private float m_InvincibilityDelay = 2f;
    [SerializeField] private float m_InvincibilityAnimationDelay = 0.8f;

    private Animator animator;
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rb;

    [SerializeField] private Image[] m_Hearts;
    [SerializeField] private Sprite m_FullHeart;
    [SerializeField] private Sprite m_EmptyHeart;

    public static PlayerLife instance;
    private void Awake()
    {
        // Ensure there is only one instance of PlayerLife in the scene
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerLife in the scene");
            return;
        }

        instance = this;

        // Set the player's health to maximum health
        m_Health = MAX_HEALTH;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
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
    public void TakeDamage()
    {
        if (!isInvincible)
        {
            m_Health -= 1;

            // Check if the player is still alive
            if (m_Health == 0)
            {
                Die();
                return;
            }

            // Make the player temporarily invincible
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    /// <summary>
    /// Handles the death of the player.
    /// </summary>
    public void Die()
    {
        // Disable player movement
        PlayerMovement.instance.enabled = false;

        // Trigger death animation
        animator.SetTrigger("Die");

        // Disable interaction with the environment
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;
        rb.velocity = Vector3.zero;

        // Call Game Over function
        GameOverManager.instance.OnPlayerDeath();
    }

    /// <summary>
    /// Respawns the player.
    /// </summary>
    public void Respawn()
    {
        // Re-enable player movement
        PlayerMovement.instance.enabled = true;

        // Trigger respawn animation
        animator.SetTrigger("Respawn");

        // Re-enable interaction with the environment
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerCollider.enabled = true;

        // Reset player health
        m_Health = MAX_HEALTH;
    }

    /// <summary>
    /// Flashes the player to indicate invincibility.
    /// </summary>
    public IEnumerator InvincibilityFlash()
    {
        if (isInvincible)
        {
            if (animator != null)
            {
                animator.SetBool("IsInvincible", true);
                yield return new WaitForSeconds(m_InvincibilityAnimationDelay);
                animator.SetBool("IsInvincible", false);
            }
        }
    }

    /// <summary>
    /// Handles the delay for player invincibility.
    /// </summary>
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(m_InvincibilityDelay);
        isInvincible = false;
    }

    
}
