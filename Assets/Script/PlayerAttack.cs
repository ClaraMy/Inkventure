using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private GameObject attackArea = default;

    private bool isAttacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackArea = transform.GetChild(1).gameObject; //changer de selector ici
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (isAttacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
                animator.SetBool("IsAttacking", isAttacking);
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        animator.SetBool("IsAttacking", isAttacking);
    }
}
