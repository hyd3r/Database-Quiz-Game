using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addUserDB : MonoBehaviour
{
    public Text status;
    public Text username;
    public InputField password;
    public DatabaseHandler database;

    public void addUserToDB()
    {
        status.text=database.addUser(username.text, password.text);
    }
}
