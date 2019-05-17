using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    private Animator anim;
    public  Movement move;
    private bool once = true;
    public Difficulty diff;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial", 1) == 0)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            move.stunned = true;
            if ((Input.GetAxisRaw("Horizontal") > 0 || move.rightDown) && once)
            {
                StartCoroutine(nextPanel(1));
            }
            if ((Input.GetAxisRaw("Horizontal") < 0 || move.leftDown) && once)
            {
                StartCoroutine(nextPanel(0));
            }
            if (Input.GetButtonDown("Jump")||move.jumpDown)
            {
                PlayerPrefs.SetInt("Tutorial", 0);
                move.stunned = false;
                diff.playGame();
                transform.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator nextPanel(int num)
    {
        once = false;
        switch (num)
        {
            case 0: anim.SetTrigger("back"); break;
            case 1: anim.SetTrigger("next"); break;
        }
        yield return new WaitForSeconds(0.5f);
        once = true;
    }
}
