using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text txt;
    private int newScore=0;

    void Update()
    {
        if (transform.position.y > newScore)
        {
            newScore = (int)transform.position.y;
        }
        txt.text = newScore.ToString();
    }
}
