using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setButton : MonoBehaviour
{
    int userid;
    string username;
    string password;
    public play play;

    public void setUser(int id, string un, string pw)
    {
        userid = id;
        username = un;
        password = pw;
        GetComponent<Text>().text = username;
    }
    public void verifyPass()
    {
        PlayerPrefs.SetInt("userid", userid);
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<play>().logi();
    }
}
