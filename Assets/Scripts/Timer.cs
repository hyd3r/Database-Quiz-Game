using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Slider timer;
    public GameManager gameManager;
    public Animator gmAnim;
    private float timerSpeed= 0.002f;
    private bool once = true;

    void Start()
    {
        timer = GetComponent<Slider>();
    }


    void Update()
    {
        if (timer.value == 0f)
        {
            if (once)
            {
                Debug.Log("WRONG");
                gmAnim.SetTrigger("False");
                once = false;

            }
            
        }
        else if(Time.timeScale!= 0.0001f)timer.value -= timerSpeed;
    }
    public void stopTimer()
    {
        timerSpeed = 0;
    }
}
