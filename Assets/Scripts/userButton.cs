using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userButton : MonoBehaviour
{
    DatabaseHandler database;

    public void selectUser()
    {
        database = FindObjectOfType<DatabaseHandler>();
        //Debug.Log(GetComponentInChildren<Text>().text);
    }

}
