using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTrig : MonoBehaviour
{
    private SceneTransitions st;
    public Transform img;
    public Transform ply;
    private Vector3 newPos;

    public Text currScore;
    public Text currScoreNew;
    public Text highScore1;
    public Text highScore2;
    public GameObject died;
    private int ppHighScore;

    private void Start()
    {
        st=GetComponent<SceneTransitions>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey("HighScore"))
            {
                ppHighScore = PlayerPrefs.GetInt("HighScore");
            }
            else {
                PlayerPrefs.SetInt("HighScore", 0);
                ppHighScore = 0;
            }


            if (ppHighScore < Int32.Parse(currScoreNew.text))
            {
                PlayerPrefs.SetInt("HighScore", Int32.Parse(currScoreNew.text));
                ppHighScore = Int32.Parse(currScoreNew.text);
            }
                
            newPos = ply.position;
            newPos.y -= 71f;
            img.position = Camera.main.WorldToScreenPoint(ply.position);

            currScore.text = currScoreNew.text;
            highScore1.text = ppHighScore.ToString();
            highScore2.text = ppHighScore.ToString();

            st.playDeathAnim();
            StartCoroutine(playAnim());

           //st.restartScene();
        }

    }
    public IEnumerator playAnim()
    {
        yield return new WaitForSeconds(1f);
        died.SetActive(true);
        died.GetComponent<Animator>().SetTrigger("deathTrig");


    }


}
