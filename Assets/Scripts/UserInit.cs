using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInit : MonoBehaviour
{
    public GameObject userButton;
    public GameObject userList;
    public DatabaseHandler database;
    public GameObject menu;
    public GameObject menu2;



    public void userInit(int game)
    {
        if(game!=0) PlayerPrefs.SetInt("game", game);

        menu2.SetActive(true);

        menu.SetActive(false);


        foreach (Transform child in userList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<account> users = database.displayUsers();
        foreach (account user in users)
        {
            var newUser = Instantiate(userButton);
            newUser.transform.SetParent(userList.transform);
            newUser.GetComponentInChildren<setButton>().setUser(user.user_ID, user.username, user.password);
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }



}
