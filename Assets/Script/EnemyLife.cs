using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    public GameObject objectToDestroy;
    private bool isAlive = true;

    private Animator animator;
    private BoxCollider2D enemyCollider;

    private int MAX_HEALTH = 1;
    private int health;
    private void Awake()
    {
        health = MAX_HEALTH;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage()
    {
        health -= 1;

        // pour vérifier si l'ennemi est toujours vivant
        if (health == 0)
        {
            Die();
            return;
        }
    }

    public void Die()
    {
        // pour activer l'animation du personnage
        animator.SetTrigger("EnemyDie");

        // pour retirer toute interaction avec l'environnement
        enemyCollider.enabled = false;
        transform.Translate(Vector3.zero);

        // pour indiquer qu'il est mort
        isAlive = false;
    }
    public bool IsAlive()
    {
        return isAlive;
    }
}
