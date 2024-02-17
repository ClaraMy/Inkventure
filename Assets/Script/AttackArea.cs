using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private EnemyLife enemyLife;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            enemyLife = collision.GetComponent<EnemyLife>();
            enemyLife.TakeDamage();
        }
    }
}
