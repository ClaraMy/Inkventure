using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private int m_DestPoint = 0;

    [SerializeField] private float m_Speed = 2.0f;

    private SpriteRenderer m_SpriteRenderer;
    private Transform m_Target;
    private EnemyLife m_EnemyLife;

    [SerializeField] private Transform[] m_Waypoints;

    void Start()
    {
        m_Target = m_Waypoints[0];
        m_EnemyLife = GetComponent<EnemyLife>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if the enemy is alive
        if (m_EnemyLife.IsAlive())
        {
            // Move towards the current target waypoint
            Vector3 dir = m_Target.position - transform.position;
            transform.Translate(dir.normalized * m_Speed * Time.deltaTime, Space.World);

            // If the enemy is close to its destination waypoint
            if (Vector3.Distance(transform.position, m_Target.position) < 0.3f)
            {
                // Switch to the next waypoint in the array
                m_DestPoint = (m_DestPoint + 1) % m_Waypoints.Length;
                m_Target = m_Waypoints[m_DestPoint];

                // Flip the sprite horizontally to change direction
                m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
            }
        }
    }

    /// <summary>
    /// Called when a collider enters this trigger collider.
    /// </summary>
    /// <param name="collision">The collider that entered this trigger collider.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collider belongs to an object tagged as "Player"
        if (collision.transform.CompareTag("Player"))
        {
            // Get the PlayerLife component from the collided player
            PlayerLife playerLife = collision.transform.GetComponent<PlayerLife>();

            // Call the TakeDamage method of the player's PlayerLife component
            playerLife.TakeDamage();
        }
    }
}
