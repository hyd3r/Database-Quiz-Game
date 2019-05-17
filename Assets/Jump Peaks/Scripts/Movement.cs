using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private float mobileInput;

    private Rigidbody2D rb;
    private Animator anim;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool bouncing = false;
    public bool stunned = false;

    public bool leftDown = false;
    public bool rightDown = false;
    public bool jumpDown = false;
    public bool jumpButton = false;

    public bool[] powerUps;
    private int numOfExtraJumps=0;
    private int extraJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        powerUpINIT();
        if (powerUps[2]) numOfExtraJumps = 3;
        else if (powerUps[1]) numOfExtraJumps = 2;
        else if (powerUps[0]) numOfExtraJumps = 1;

        if (powerUps[4]) jumpForce += 2f;
        if (powerUps[5]) jumpForce += 2f;
        if (powerUps[6]) jumpForce += 2f;

        if (powerUps[3]) GetComponent<EnemyCollision>().shield = 3;

        extraJumps = numOfExtraJumps;
    }

    public void leftMoveUp() { leftDown = false; }
    public void rightMoveUp() { rightDown = false; }
    public void leftMoveDown() { leftDown = true; }
    public void rightMoveDown() { rightDown = true; }
    public void jumpMoveUp() { jumpDown = false; }
    public void jumpMoveDown() { jumpDown = true; }
    public void jumpButtonDown() { jumpButton = true; }

    public void playerStunned(float x)
    {
        StartCoroutine(delayStunned());
        moveInput=0;
        if (transform.position.x > x) moveInput = 1;
        else moveInput = -1;
        rb.velocity = new Vector2(rb.velocity.x, 4f);

    }
    IEnumerator delayStunned()
    {
        stunned = true;
        yield return new WaitForSeconds(0.2f); 
        moveInput = 0;
        yield return new WaitForSeconds(0.5f);
        stunned = false;
    }

    void Update()
    {
        if (!stunned)
        {
            if ((Input.GetButtonDown("Jump") || jumpButton) &&isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("jump", true);
            }
            else if((Input.GetButtonDown("Jump") || jumpButton) && !isGrounded&&extraJumps>0)
            {
                isJumping = true;
                extraJumps--;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("jump", true);
            }
            if ((Input.GetButton("Jump") || jumpDown) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else isJumping = false;

            }
            if ((Input.GetButtonDown("Jump") || jumpButton)) { isJumping = false; jumpButton = false; }
            if (rb.velocity.y < 0 && bouncing == false)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !(Input.GetButton("Jump") || jumpDown) && bouncing == false)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            if (isGrounded)
            {
                bouncing = false;
            }
            if(isGrounded&&rb.velocity.y==0f) extraJumps = numOfExtraJumps;
            if (rb.velocity.y == 0f)
            {
                anim.SetBool("jump", false);
            }
        }
    }

    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        if (!stunned) moveInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveInput) > 0 && rb.velocity.y == 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (leftDown)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            if (facingRight == true)
            {
                Flip();
            }

        }
        else if (rightDown)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            if (facingRight == false)
            {
                Flip();
            }
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void powerUpINIT()
    {
        for (int i = 0; i < powerUps.Length; i++)
        {
            switch (i)
            {
                case 0:
                    switch (PlayerPrefs.GetInt("PowerUp1"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 1:
                    switch (PlayerPrefs.GetInt("PowerUp2"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 2:
                    switch (PlayerPrefs.GetInt("PowerUp3"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 3:
                    switch (PlayerPrefs.GetInt("PowerUp4"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 4:
                    switch (PlayerPrefs.GetInt("PowerUp5"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 5:
                    switch (PlayerPrefs.GetInt("PowerUp6"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
                case 6:
                    switch (PlayerPrefs.GetInt("PowerUp7"))
                    {
                        case 0:
                            powerUps[i] = false;
                            break;
                        case 1:
                            powerUps[i] = true;
                            break;
                    }
                    break;
            }
        }
    }
}
