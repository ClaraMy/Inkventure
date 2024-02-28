using UnityEngine;
public class AttackArea : MonoBehaviour
{
    private EnemyLife m_EnemyLife;

    /// <summary>
    /// Called when a collider enters this trigger collider.
    /// </summary>
    /// <param name="collision">The collider that entered this trigger collider.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to an object tagged as "Enemy"
        if (collision.transform.CompareTag("Enemy"))
        {
            // Get the EnemyLife component from the collided enemy
            m_EnemyLife = collision.GetComponent<EnemyLife>();

            // Call the TakeDamage method of the enemy's EnemyLife component
            m_EnemyLife.TakeDamage();
        }
    }
}
