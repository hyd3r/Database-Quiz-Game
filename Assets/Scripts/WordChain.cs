using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class WordChain : MonoBehaviour
{
    List<string> strLines;
    TextAsset myTxt;
    public Text wordInput;
    public Text wordInput2;
    public Text wordDisp;
    Dictionary<char, Dictionary<String,String>> dict;
    Dictionary<string, string> lockedWords;
    public InputField txt;
    private int wordTotalScore=0;
    private float timeAmount = 10f;
    private float timeAmountReset = 1f;
    private float time;
    private float timeReset;
    private string tempText;
    public Image timerSlider;
    private string currentWord;
    private bool resetting = false;
    List<char> keyList;
    List<string> nestedKeyList;
    System.Random rand;
    private bool startTimer = false;
    public Text score;
    private float increaseDifficulty = 0.2f;
    public Animator scoreAnim;
    public AudioSource audios;
    public AudioClip clip;

    private List<string> TextAssetToList(TextAsset ta)
    {
        var splitFile = new string[] { "\r\n", "\r", "\n" };
        return new List<string>(ta.text.Split(splitFile, StringSplitOptions.None));
    }
    void Start()
    {
        time = timeAmount;
        wordTotalScore = 0;
        myTxt = Resources.Load("words_alpha") as TextAsset;
        strLines = TextAssetToList(myTxt);
        dict = new Dictionary<char, Dictionary<String, String>>();
        dictInit(strLines);
    }
    private void highlightLetter(string word)
    {
        currentWord = word;
        tempText = word[word.Length - 1] + "";
        word=word.Remove(word.Length - 1);
        word += "<i><b>" + tempText + "</b></i>";
        wordDisp.text = word;
    }

    private void nextWord(char endsWith)
    {
        nestedKeyList = dict[endsWith].Keys.ToList();
        highlightLetter(nestedKeyList[rand.Next(nestedKeyList.Count)]);
    
    }
    private string wordScore(string word)
    {
        int wordBonus;
        wordBonus = word.Length - 3;
        //time = timeAmount;
        timeReset = time / timeAmount;
        resetting = true;
        timeAmount -= increaseDifficulty;
        int addScore= wordBonus + (int)Math.Floor(time);
        wordTotalScore += addScore;
        scoreAnim.SetTrigger("addScore");
        return "<size=100><b>" + wordTotalScore + "</b></size>\n<size=20><i>+" + addScore+"</i></size>";
        
    }

    private void wordGameOver()
    {
        PlayerPrefs.SetInt("score", wordTotalScore);
        PlayerPrefs.SetString("currentGame","Shiritori");
        PlayerPrefs.SetInt("gameid", 2);
        SceneManager.LoadScene("Finish");
    }

    IEnumerator colorDisp(bool isCorrect)
    {
        if (isCorrect)
        {
            audios.PlayOneShot(clip);
            wordInput2.color = new Color32(255, 198, 0, 255);
            score.text = wordScore(wordInput.text);
            yield return new WaitForSeconds(1f);
            wordInput2.color = new Color32(33, 33, 33, 255);
            nextWord(wordInput.text[wordInput.text.Length - 1]);
            txt.text = "";
            txt.ActivateInputField();
        }
        else
        {
            wordInput2.color = new Color32(199, 0, 0, 255);
            yield return new WaitForSeconds(0.5f);
            wordInput2.color = new Color32(33, 33, 33, 255);
            txt.ActivateInputField();
            txt.Select();
        }
    }

    IEnumerator timerDelay()
    {
        timeReset += Time.deltaTime;
        timerSlider.fillAmount = timeReset / timeAmountReset;
        if (timerSlider.fillAmount == 1)
        {
            yield return new WaitForSeconds(0.3f);
            resetting = false;
            time = timeAmount;
        }
    }

    void Update()
    {
        if (txt.isFocused) startTimer = true;
        if (Input.GetButtonDown("Submit"))
        {  
            if (currentWord[currentWord.Length - 1].Equals(wordInput.text[0]))
            {
                if (dict[wordInput.text[0]].ContainsKey(wordInput.text.ToString()) && !lockedWords.ContainsKey(wordInput.text.ToString()))
                {
                    lockedWords[wordInput.text.ToString()] = "";
                    StartCoroutine(colorDisp(true));
                }
                else
                {
                    StartCoroutine(colorDisp(false));
                }
            }
            else
            {
                StartCoroutine(colorDisp(false));
            }
        }
        if (resetting)
        {
            StartCoroutine(timerDelay());
        }
        else if (timerSlider.fillAmount == 0)
        {
            wordGameOver();
        }
        else if (time > 0&&startTimer)
        {
            time -= Time.deltaTime;
            timerSlider.fillAmount = time / timeAmount;
        }
    }
    private void dictInit(List<string> wordsList)
    {
        dict['b'] = new Dictionary<string, string>();
        dict['a'] = new Dictionary<string, string>();
        dict['c'] = new Dictionary<string, string>();
        dict['d'] = new Dictionary<string, string>();
        dict['e'] = new Dictionary<string, string>();
        dict['f'] = new Dictionary<string, string>();
        dict['g'] = new Dictionary<string, string>();
        dict['h'] = new Dictionary<string, string>();
        dict['i'] = new Dictionary<string, string>();
        dict['j'] = new Dictionary<string, string>();
        dict['k'] = new Dictionary<string, string>();
        dict['l'] = new Dictionary<string, string>();
        dict['m'] = new Dictionary<string, string>();
        dict['n'] = new Dictionary<string, string>();
        dict['o'] = new Dictionary<string, string>();
        dict['p'] = new Dictionary<string, string>();
        dict['q'] = new Dictionary<string, string>();
        dict['r'] = new Dictionary<string, string>();
        dict['s'] = new Dictionary<string, string>();
        dict['t'] = new Dictionary<string, string>();
        dict['u'] = new Dictionary<string, string>();
        dict['v'] = new Dictionary<string, string>();
        dict['w'] = new Dictionary<string, string>();
        dict['x'] = new Dictionary<string, string>();
        dict['y'] = new Dictionary<string, string>();
        dict['z'] = new Dictionary<string, string>();

        foreach (string sort in wordsList)
        {
            switch (sort[0])
            {
                case 'a': dict['a'][sort] = ""; break;
                case 'b': dict['b'][sort] = ""; break;
                case 'c': dict['c'][sort] = ""; break;
                case 'd': dict['d'][sort] = ""; break;
                case 'e': dict['e'][sort] = ""; break;
                case 'f': dict['f'][sort] = ""; break;
                case 'g': dict['g'][sort] = ""; break;
                case 'h': dict['h'][sort] = ""; break;
                case 'i': dict['i'][sort] = ""; break;
                case 'j': dict['j'][sort] = ""; break;
                case 'k': dict['k'][sort] = ""; break;
                case 'l': dict['l'][sort] = ""; break;
                case 'm': dict['m'][sort] = ""; break;
                case 'n': dict['n'][sort] = ""; break;
                case 'o': dict['o'][sort] = ""; break;
                case 'p': dict['p'][sort] = ""; break;
                case 'q': dict['q'][sort] = ""; break;
                case 'r': dict['r'][sort] = ""; break;
                case 's': dict['s'][sort] = ""; break;
                case 't': dict['t'][sort] = ""; break;
                case 'u': dict['u'][sort] = ""; break;
                case 'v': dict['v'][sort] = ""; break;
                case 'w': dict['w'][sort] = ""; break;
                case 'x': dict['x'][sort] = ""; break;
                case 'y': dict['y'][sort] = ""; break;
                case 'z': dict['z'].Add(sort,"z" ); break;
            }
        }
        lockedWords = new Dictionary<string, string>();
        keyList = new List<char>(dict.Keys);
        rand = new System.Random();
        nestedKeyList = new List<string>(dict[keyList[rand.Next(keyList.Count)]].Keys);
        highlightLetter(nestedKeyList[rand.Next(nestedKeyList.Count)]);
    }
}
