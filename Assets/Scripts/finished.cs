using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finished : MonoBehaviour
{
    private Text txt;
    public DatabaseHandler dh;
    
    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "\n\n"+PlayerPrefs.GetString("username").ToString() +"\n<size=90>" + PlayerPrefs.GetInt("score").ToString()+"</size>";
        dh.addScore();
    }

    IEnumerator buttonDelay(int state)
    {
        switch (state)
        {
            case (1):
                yield return new WaitForSeconds(0.15f);
                switch (PlayerPrefs.GetString("currentGame"))
                {
                    case ("Shiritori"): SceneManager.LoadScene("MainGame2"); break;
                    case ("Trivia"): SceneManager.LoadScene("MainGame"); break;
                }
                break;
            case (2):
                yield return new WaitForSeconds(0.15f);
                SceneManager.LoadScene("MainMenu");
                break;
        }


    }
    public void restartGame()
    {
        StartCoroutine(buttonDelay(1));
    }
    public void backToMainMenu()
    {
        StartCoroutine(buttonDelay(2));
    }
}
