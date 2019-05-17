using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGinfinite : MonoBehaviour
{
    private Rigidbody2D rb;
    private float bgSpeed = -1.5f;
    private BoxCollider2D bgTrig;
    private float bgSize;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, bgSpeed);
        bgTrig = GetComponent<BoxCollider2D>();
        bgSize = bgTrig.size.x;
    }


    void Update()
    {
        if (transform.position.y < -bgSize)
        {
            repeateBackground();
        }
    }

    void repeateBackground()
    {
        Vector2 bgOffset = new Vector2(0, bgSize * 2f);
        transform.position = (Vector2)transform.position + bgOffset;
    }
}
