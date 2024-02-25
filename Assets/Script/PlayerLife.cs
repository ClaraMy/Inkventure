using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D playerCollider;
    public static int health;
    private int maxHealth = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float invincibilityDelay = 2f;
    public float invincibilityAnimationDelay = 0.8f;
    public bool isInvincible = false;
    public static PlayerLife instance;
    private void Awake()
    {
        // pour vérifier qu'il n'y a qu'une seule instance de PlayerLife dans la scène
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de PlayerLife dans la scène");
            Destroy(gameObject);
        }

        health = maxHealth;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void TakeDamage()
    {
        if (!isInvincible)
        {
            health -= 1;

            // pour vérifier si le joueur est toujours vivant
            if(health == 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        // pour bloquer les mouvements du personnage
        PlayerMovement.instance.enabled = false;

        // pour activer l'animation du personnage
        PlayerMovement.instance.animator.SetTrigger("Die");

        // pour retirer toute interaction avec l'environnement
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;
        PlayerMovement.instance.rb.velocity = Vector3.zero;

        // pour appeler la fonction d'apparation du Game Over
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        // pour réactiver les mouvements du personnage
        PlayerMovement.instance.enabled = true;

        // pour activer l'animation du personnage
        PlayerMovement.instance.animator.SetTrigger("Respawn");

        // pour réactiver les interactions du joueur avec l'environnement
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;

        // pour remettre la vie au joueur
        health = maxHealth;
    }

    public IEnumerator InvincibilityFlash()
    {
        if (isInvincible)
        {
            if (animator != null)
            {
                animator.SetBool("IsInvincible", true);
                yield return new WaitForSeconds(invincibilityAnimationDelay);
                animator.SetBool("IsInvincible", false);
            }
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityDelay);
        isInvincible = false;
    }

    
}
