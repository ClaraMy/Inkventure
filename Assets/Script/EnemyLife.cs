using UnityEngine;

public class EnemyLife : CharacterLife
{
    private new int m_Health = 1;

    private bool m_IsAlive = true;

    private BoxCollider2D m_EnemyCollider;

    protected override void Start()
    {
        base.Start();
        m_EnemyCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    public override void TakeDamage()
    {
        base.TakeDamage();
    }

    /// <summary>
    /// Handles the death of the enemy.
    /// </summary>
    public override void Die()
    {
        // Trigger death animation
        TriggerDeathAnimation("EnemyDie");

        // Disable interaction with the environment
        DisableCollider(m_EnemyCollider);
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
