using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public question[] questions;
    public static List<question> unansweredQuestions;
    public DatabaseHandler dh;
    public Timer timer;
    public Text details;
    public RectTransform progressObject;
    public GameObject progressTabObject;
    float progressTabSize;
    public static List<string> answered;

    private question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 0.2f;

    [SerializeField]
    private Animator animator;

    public static int score;
    public static int currentItem=1;
    public static int allItems;

    private bool once = true;

    public AudioSource audioSRC;
    public AudioClip correctSFX;
    public AudioClip questionSFX;
    public AudioClip timeUpSFX;
    public AudioClip wrongSFX;

    void Start()
    {
        questions = new question[dh.getRowCount()];

        dh.getQuestions().CopyTo(questions, 0);

        if (unansweredQuestions == null)
        {
            unansweredQuestions = questions.ToList<question>();
            score = 0;
            allItems = dh.getRowCount();
            answered = new List<string>();
        }

        if (unansweredQuestions.Count == 0)
        {
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetString("currentGame", "Trivia");
            PlayerPrefs.SetInt("gameid", 1);
            SceneManager.LoadScene("Finish");
        }
        else SetCurrentQuestion();

        audioSRC.PlayOneShot(questionSFX);

        progressObject = GameObject.Find("ProgressObject").GetComponent<RectTransform>();
        progressTabObject = GameObject.Find("ProgressObject/Tab");
        for (int index = 0; index < allItems; index++)
        {
            Transform newTab = Instantiate(progressTabObject.transform) as Transform;
            newTab.SetParent(progressObject.transform);
            newTab.localScale = Vector3.one;
            newTab.localPosition = Vector3.one;
            newTab.Find("Text").GetComponent<Text>().text = (index + 1).ToString();
            if (index < answered.Count)
            {
                switch (answered[index])
                {
                    case "Correct": newTab.GetComponent<Image>().color = Color.green; break;
                    case "Wrong": newTab.GetComponent<Image>().color = Color.red; break;
                }
            }
            else if (index != answered.Count) newTab.GetComponent<Image>().color = Color.grey;
        }
        progressTabSize = progressObject.GetComponent<GridLayoutGroup>().cellSize.x;
        progressTabObject.SetActive(false);


    }

    void Update()
    {
        if (progressObject && progressTabObject) progressObject.anchoredPosition = new Vector2(Mathf.Lerp(progressObject.anchoredPosition.x, progressTabSize * (0.5f - currentItem), Time.deltaTime * 10), progressObject.anchoredPosition.y);

    }

    void SetCurrentQuestion()
    {
        int randomQuestionsIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionsIndex];

        factText.text = currentQuestion.fact;
        details.text = currentQuestion.details;

    }

    public IEnumerator TranstionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        animator.SetTrigger("nextRound");
        currentItem++;

        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void next()
    {
        unansweredQuestions.Remove(currentQuestion);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void userSelectTrue()
    {

        if (currentQuestion.isTrue)
        {
            if (once)
            {
                Debug.Log("Correct");
                animator.SetTrigger("True");
                increaseScore();
                once = false;
                audioSRC.PlayOneShot(correctSFX);
                answered.Add("Correct");
            }
        }
        else
        {
            if (once)
            {
                Debug.Log("WRONG");
                animator.SetTrigger("False");
                once = false;
                audioSRC.PlayOneShot(wrongSFX);
                answered.Add("Wrong");
            }
        }
        timer.stopTimer();
        //StartCoroutine (TranstionToNextQuestion());
    }

    public void userSelectFalse()
    {

        if (!currentQuestion.isTrue)
        {
            if (once)
            {
                Debug.Log("Correct");
                animator.SetTrigger("False");
                increaseScore();
                once = false;
                audioSRC.PlayOneShot(correctSFX);
                answered.Add("Correct");
            }

        }
        else
        {
            if (once)
            {
                Debug.Log("WRONG");
                animator.SetTrigger("True");
                once = false;
                audioSRC.PlayOneShot(wrongSFX);
                answered.Add("Wrong");
            }
        }
        timer.stopTimer();
        //StartCoroutine (TranstionToNextQuestion());
    }

    public void increaseScore()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }
    public int getItems()
    {
        return allItems;
    }
}
