using UnityEngine;
using System.Collections;
public abstract class CharacterLife : MonoBehaviour
{
    protected int m_Health;

    protected Animator m_Animator;

    protected virtual void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Deals damage to the character.
    /// </summary>
    public virtual void TakeDamage()
    {
        m_Health -= 1;

        // Check if the character is still alive
        if (m_Health <= 0)
        {
            Die();
            return;
        }
    }

    /// <summary>
    /// Handles the death of the character.
    /// </summary>
    public abstract void Die();

    protected virtual void TriggerDeathAnimation(string triggerName)
    {
        if (m_Animator != null)
        {
            m_Animator.SetTrigger(triggerName);
        }
    }

    protected virtual void DisableCollider(Collider2D collider)
    {
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
