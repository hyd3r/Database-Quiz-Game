using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class userLogin : MonoBehaviour
{
    public Text status;
    public InputField password;

    public void login()
    {
        if (PlayerPrefs.GetString("password").Equals(password.text))
        {
            status.text = "Login Successful";
            StartCoroutine(delayPlay());
        }
        else
        {
            status.text = "Incorrect Password";
        }
    }
    IEnumerator delayPlay()
    {
       
        yield return new WaitForSeconds(0.5f);
        switch (PlayerPrefs.GetInt("game"))
        {
            case 1: SceneManager.LoadScene("MainGame"); break;
            case 2: SceneManager.LoadScene("MainGame2"); break;
            case 3: PlayerPrefs.SetInt("Tutorial", 1);
                SceneManager.LoadScene("jp");
                break;
        }
        
    }
    }
