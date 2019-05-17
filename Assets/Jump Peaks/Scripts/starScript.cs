using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starScript : MonoBehaviour
{
    public Text starText;
    public GameObject starPickup;
    public GameObject enemyHit;
    private int stars;
    private int heightStar=50;
    void Start()
    {
        stars=PlayerPrefs.GetInt("Stars", 0);
        starText.text = stars.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Instantiate(starPickup, collision.transform.position,Quaternion.identity);
            increaseStar();
            Destroy(collision.gameObject);
        }
    }
    public void increaseStar()
    {
        stars++;
        starText.text = stars.ToString();
        PlayerPrefs.SetInt("Stars", stars);
    }
    void Update()
    {
        if (transform.position.y > heightStar)
        {
            increaseStar();
            heightStar += 50;
        }
    }
}
