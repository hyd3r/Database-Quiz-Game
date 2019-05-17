using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFly : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public bool moveUp=true;
    public float flyHeight;
    private float height;
    public bool stomped = false;
    public GameObject killSFX;
    public GameObject playerHitSFX;
    public int state;

    public bool moveRight;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hitsWall;

    public GameObject playerDetection;
    private Transform playerLoc = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        height = transform.position.y;
    }

    public void enemyStomped()
    {
        stomped = true;
        StartCoroutine(delayKill());
    }
    
    void Update()
    {
        if (!stomped)
        {
            switch (state)
            {
                case 1:
                    hitsWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
                    if (hitsWall) moveRight = !moveRight;

                    if (moveRight)
                    {
                        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
                        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    }

                    break;
                case 2:
                    if (transform.position.y > height + flyHeight || transform.position.y < height - flyHeight)
                    {
                        moveUp = !moveUp;
                    }
                    if (moveUp) rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
                    else rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
                    break;
                case 3:
                    playerLoc = playerDetection.GetComponent<enemyPlayerDetection>().playerLocation();
                    if (playerLoc != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, 1 * Time.deltaTime);
                    }
                    break;
            }
        }
        else rb.velocity = new Vector2(0f, 0f) ;
    }
    IEnumerator delayKill()
    {
        GameObject sfx=Instantiate(killSFX, transform.position, Quaternion.identity);
        sfx.transform.localScale = new Vector3(sfx.transform.localScale.x * 1.5f, sfx.transform.localScale.y * 3, sfx.transform.localScale.z * 1.5f);
        transform.localScale = new Vector3(0.3f, 0.15f, 0.3f);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
