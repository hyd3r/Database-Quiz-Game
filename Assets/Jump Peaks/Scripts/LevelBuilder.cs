using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public GameObject[] enemies;
    public Transform playerChar;
    public float levelWidth = 1.8f;
    public float minY = .2f;
    public float maxY = 1.5f;
    public Vector3 spawnPosition = new Vector3();
    public float increaseDifficulty = 50f;
    public GameObject stars;

    
    void Update()
    {
        if ((spawnPosition.y-playerChar.position.y)<10)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            
            Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)], spawnPosition, Quaternion.identity);
            switch (Random.Range(0, 20))
            {
                case 0: Instantiate(stars, new Vector2(Random.Range(-levelWidth, levelWidth), spawnPosition.y + Random.Range(minY, maxY)), Quaternion.identity); break;
            }
            switch (Random.Range(0, 40))
            {
                case 0: Instantiate(enemies[0], new Vector2(Random.Range(-levelWidth, levelWidth), spawnPosition.y + Random.Range(minY, maxY)), Quaternion.identity); break;
                case 1: Instantiate(enemies[1], new Vector2(Random.Range(-levelWidth, levelWidth), spawnPosition.y + Random.Range(minY, maxY)), Quaternion.identity); break;
                case 2: Instantiate(enemies[2], new Vector2(Random.Range(-levelWidth, levelWidth), spawnPosition.y + Random.Range(minY, maxY)), Quaternion.identity); break;
            }

        }
        if (spawnPosition.y > increaseDifficulty)
        {
            increaseDifficulty += 25f;
            Difficulty.increaseDifficulty();
        }
    }
}
