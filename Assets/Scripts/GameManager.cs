using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{

    private bool isStart = false;
    private bool isCountDown = false;
    private bool isGameFinish = false;
    public GameObject countDown;
    public GameObject game;
    public Text timer;
    public Text leftText;
    public Text rightText;
    public Image progressBar;
    public GameObject result;
    public Text signOCount;
    public Text signXCount;
    public GameObject signO;
    public GameObject signX;
    public AudioSource music;
    public AudioSource click;
    public AudioSource Osound;
    public AudioSource Xsound;
    public AudioSource countdown;
    public AudioSource finish;

    public List<Question> QuestionList = new List<Question>();
    private List<int> useQuestionList = new List<int>();

    private System.Random randomObj = new System.Random();

    private int quesitonCount = 0;
    private float count = 5.0f;

    private bool leftPlayer = false;
    private bool rightPlayer = false;
    private int oCount = 0;
    private int xCount = 0;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountDown || isGameFinish)
        {
            return;
        }
        if (!isStart)
        {
            if (Input.anyKeyDown)
            {
                isStart = true;
                StartGame();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            click.Stop();
            click.Play();
            leftPlayer = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            click.Stop();
            click.Play();
            leftPlayer = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            click.Stop();
            click.Play();
            rightPlayer = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            click.Stop();
            click.Play();
            rightPlayer = true;
        }

        count -= Time.deltaTime;
        progressBar.fillAmount = count / 5;
        timer.text = ((int)count + 1).ToString();
        if (count < 0)
        {
            if(quesitonCount >= 7)
            {
                music.Stop();
                finish.Play();
                if (leftPlayer == rightPlayer)
                {
                    oCount++;
                }
                else
                {
                    xCount++;
                }
                signOCount.text = oCount.ToString() + "°³";
                signXCount.text = xCount.ToString() + "°³";
                game.SetActive(false);
                result.SetActive(true);
                isGameFinish = true;
                return;
            }
            if(leftPlayer == rightPlayer)
            {
                oCount++;
                Osound.Play();
                signO.SetActive(true);
                Invoke("removeSignO", 1f);
            }
            else
            {
                xCount++;
                Xsound.Play();
                signX.SetActive(true);
                Invoke("removeSignX", 1f);
            }
            count = 5.0f;
            UpdateQuestion();
        }
    }

    void StartGame()
    {
        UpdateQuestion();
        countdown.Play();
        StartCoroutine(startCountDown());
        Invoke("enableGameUI", 3f);
    }

    private void enableGameUI()
    {
        game.SetActive(true);
        music.Play();
    }

    private IEnumerator startCountDown()
    {
        countDown.SetActive(true);
        isCountDown = true;
        for(int i = 3; i > 0; i--)
        {
            countDown.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDown.SetActive(false);
        isCountDown = false;
    }


    private void UpdateQuestion()
    {
        int index;
        do {
            index = randomObj.Next(0, (QuestionList.Count - 1));
        } while (useQuestionList.Contains(index));

        Question question = QuestionList[index];
        useQuestionList.Add(index);
        quesitonCount ++;
        leftText.text = question.questionLeft;
        rightText.text = question.questionRight;
    }

    private void removeSignO()
    {
        signO.SetActive(false);
    }

    private void removeSignX()
    {
        signX.SetActive(false);
    }

    public void onClickGoToMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}

[Serializable]
public struct Question
{
    public string questionLeft;
    public string questionRight;

    public Question(string questionLeft, string questionRight)
    {
        this.questionLeft = questionLeft;
        this.questionRight = questionRight;
    }
}
