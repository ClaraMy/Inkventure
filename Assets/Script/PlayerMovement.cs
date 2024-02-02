using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float m_Move;
    public float m_Speed;
    public float m_Jump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(m_Move * m_Speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, m_Jump * 10));
        }
    }
}
