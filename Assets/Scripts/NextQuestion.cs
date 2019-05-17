using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuestion : MonoBehaviour
{
    public GameManager gm;

    public void nextQuestion()
    {
        StartCoroutine(gm.TranstionToNextQuestion());
    }
}
