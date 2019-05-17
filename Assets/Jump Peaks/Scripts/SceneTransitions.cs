using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    public Animator transitionAnim;
    public string sceneName;

 
    public void restartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void playDeathAnim()
    {
        Difficulty.resetDifficulty();
        transitionAnim.SetTrigger("end");
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LoadScene()
    {
        
        yield return new WaitForSeconds(1.5f);
        
    }

}
