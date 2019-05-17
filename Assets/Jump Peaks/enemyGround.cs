using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGround : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public bool moveRight;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hitsWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        hitsWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hitsWall) moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(0.3f, transform.localScale.y, transform.localScale.z);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(-0.3f, transform.localScale.y, transform.localScale.z);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }
}
