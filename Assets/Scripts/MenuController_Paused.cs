using UnityEngine;
using System.Collections;

public class MenuController_Paused : MonoBehaviour 
{

	public bool isPaused; 
	public string buttonToTogglePause;
	public string canvasIndex; 
	public string startingIndex; 
	public bool useCursorLock;
    public GameObject go;

    public enum MenuTypes
	{
		 unity2D
	}

	public MenuTypes menuType;

	void Start() 
	{
		canvasIndex = startingIndex; 
	}

	void Update()
	{
		if (Input.GetButtonDown (buttonToTogglePause) && isPaused == false) {
			isPaused = true;
			CheckPause ();
		}else if (Input.GetButtonDown(buttonToTogglePause) && isPaused == true)
        {
            isPaused = false;
            CheckPause();
        }

    }

	public void CheckPause()
	{
		if (isPaused) {
			Time.timeScale = 0.0001f;
            go.SetActive(true);
            if (useCursorLock) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		if (!isPaused) {
			Time.timeScale = 1;
			canvasIndex = startingIndex;
            go.SetActive(false);
            if (useCursorLock) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}
}
