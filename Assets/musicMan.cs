using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicMan : MonoBehaviour
{
    float currentMusicTime;
    public AudioSource audios;


    void Update()
    {
        currentMusicTime = audios.time;
        PlayerPrefs.SetFloat("currentMusicTime", currentMusicTime);
    }
    private void OnLevelWasLoaded(int level)
    {
        currentMusicTime = PlayerPrefs.GetFloat("currentMusicTime");
        currentMusicTime += 0.1f;
        audios.time = currentMusicTime;
    }

}
