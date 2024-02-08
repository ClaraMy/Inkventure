using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    public static int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float invincibilityDelay = 2f;
    public float invincibilityAnimationDelay = 0.8f;
    public bool isInvincible = false;


    private void Awake()
    {
        health = 3;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
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
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
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
