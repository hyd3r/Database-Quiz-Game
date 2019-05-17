using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public GameObject levelBuilder;

    private void Start()
    {
        levelBuilder = GameObject.FindGameObjectWithTag("LevelBuild");
    }
    void Update()
    {
        if ((levelBuilder.GetComponent<LevelBuilder>().spawnPosition.y - this.transform.position.y) > 35){
            Destroy(this.gameObject);
        }
    }
}
