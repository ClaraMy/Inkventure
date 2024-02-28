using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float m_Speed = 2.0f;
    [SerializeField] private Transform[] waypoints;

    public SpriteRenderer pig1;
    private Transform target;
    private int destPoint = 0;
    private EnemyLife enemyLife;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
        enemyLife = GetComponent<EnemyLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLife.IsAlive())
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * m_Speed * Time.deltaTime, Space.World);

            // Si l'ennemi est presque arrivé à sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
                pig1.flipX = !pig1.flipX;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.transform.GetComponent<PlayerLife>();
            playerLife.TakeDamage();
        }
    }
}
