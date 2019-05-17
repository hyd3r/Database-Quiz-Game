using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    GameObject go;

    void Start()
    {
        go = GameObject.FindGameObjectWithTag("pw");
        go.SetActive(false);
    }

    public void logi()
    {
        go.SetActive(true);
    }

    public void playGame()
    {
        StartCoroutine(delayPlay());
    }
    IEnumerator delayPlay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainGame");
    }
}
