using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPlayerDetection : MonoBehaviour
{
    private Transform player=null;

    public Transform playerLocation()
    {
        return player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            player = collision.transform;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }
}
