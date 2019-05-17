using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboards : MonoBehaviour
{
    public DatabaseHandler dh;
    public GameObject userTab;
    public GameObject userScrollList;
    public GameObject userScrollList2;
    List<score>[] score;
    int game1rank = 0;
    int game2rank = 0;

    void Start()
    {
        score = dh.displayScores();
        foreach (Transform child in userScrollList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in userScrollList2.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (score user in score[0])
        {
            var newUser = Instantiate(userTab);
            newUser.transform.SetParent(userScrollList.transform);
            int i = 0;
            game1rank++;
            foreach (Transform child in newUser.transform)
            {
                i++;
                switch (i)
                {
                    case 1: child.gameObject.GetComponent<Text>().text = game1rank.ToString(); break;
                    case 2: child.gameObject.GetComponent<Text>().text = user.user; break;
                    case 3: child.gameObject.GetComponent<Text>().text = user.scored.ToString(); break;
                    case 4: child.gameObject.GetComponent<Text>().text = user.date; break;
                }

            }
        }
        foreach (score user in score[1])
        {
            var newUser = Instantiate(userTab);
            newUser.transform.SetParent(userScrollList2.transform);
            int i = 0;
            game2rank++;
            foreach (Transform child in newUser.transform)
            {
                i++;
                switch (i) {
                    case 1: child.gameObject.GetComponent<Text>().text = game2rank.ToString(); break;
                    case 2: child.gameObject.GetComponent<Text>().text = user.user; break;
                    case 3: child.gameObject.GetComponent<Text>().text = user.scored.ToString(); break;
                    case 4: child.gameObject.GetComponent<Text>().text = user.date; break;
                }
                
            }
        }
    }
}
