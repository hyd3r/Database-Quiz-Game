using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static float difficultySpeed = 0f;
    private Vector3 deathTrigPos;

    void Start()
    {
        if (PlayerPrefs.GetInt("Tutorial", 1) == 0) playGame();
    }
    public void playGame()
    {
        StartCoroutine(delayStart());
    }
    
    void Update()
    {
        deathTrigPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(0f, deathTrigPos.y + difficultySpeed * Time.deltaTime,0f);
    }

    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(2);
        difficultySpeed = 2f;
    }

    public static void increaseDifficulty()
    {
        if (difficultySpeed < 8f)
        {
            difficultySpeed += .2f;
        }
    }

    public static void resetDifficulty()
    {

        difficultySpeed = 0f;

    }

    public float getDifficulty()
    {
        return difficultySpeed;
    }

}
