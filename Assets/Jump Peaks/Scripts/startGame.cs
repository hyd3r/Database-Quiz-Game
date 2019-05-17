using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
 
    public void startTheGame()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        SceneManager.LoadScene("jp");
    }

}
