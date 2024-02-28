using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private const int MAX_HEALTH = 1;
    private int m_Health;
    
    private bool m_IsAlive = true;

    private Animator m_Animator;
    private BoxCollider2D m_EnemyCollider;
   
    private void Awake()
    {
        // Initialize health to the maximum value
        m_Health = MAX_HEALTH;
    }
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_EnemyCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    public void TakeDamage()
    {
        m_Health -= 1;

        // Check if the enemy is still alive
        if (m_Health == 0)
        {
            Die();
            return;
        }
    }

    /// <summary>
    /// Handles the death of the enemy.
    /// </summary>
    public void Die()
    {
        // Trigger death animation
        m_Animator.SetTrigger("EnemyDie");

        // Disable interaction with the environment
        m_EnemyCollider.enabled = false;
        transform.Translate(Vector3.zero);

        // Mark the enemy as dead
        m_IsAlive = false;
    }

    /// <summary>
    /// Checks if the enemy is alive.
    /// </summary>
    /// <returns>True if the enemy is alive, otherwise false.</returns>
    public bool IsAlive()
    {
        return m_IsAlive;
    }
}
