using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int shield = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.y = 30f;
            GetComponent<Rigidbody2D>().velocity = velocity;
            GetComponent<Movement>().bouncing = true;
            collision.GetComponent<enemyFly>().enemyStomped();
            GetComponent<starScript>().increaseStar();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (shield<=0)
            {
                GetComponent<Movement>().playerStunned(collision.collider.transform.position.x);
                GameObject sfx = Instantiate(collision.collider.GetComponent<enemyFly>().playerHitSFX, collision.transform.position, Quaternion.identity);
                sfx.transform.localScale = new Vector3(sfx.transform.localScale.x * 4, sfx.transform.localScale.y * 2, sfx.transform.localScale.z * 2);
            }
            else
            {
                shield--;
                Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
                velocity.y = 30f;
                GetComponent<Rigidbody2D>().velocity = velocity;
                GetComponent<Movement>().bouncing = true;
                collision.collider.GetComponent<enemyFly>().enemyStomped();
                collision.collider.GetComponent<Collider2D>().enabled = false;
                GetComponent<starScript>().increaseStar();
            }
        }
    }
}
