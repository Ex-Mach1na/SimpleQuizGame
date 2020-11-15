using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Questions")]
    public List<Question> questionList = new List<Question>();
    //UI Reference
    [Header("UI Refference")]
    public Text QuestionText;
    public Text ScoreText;
    public Text TimerText;
    public Text[] OptionText = new Text[4];
    public Button[] Buttons = new Button[4];
    public Canvas gamerunningUi;
    public GameObject gameFinishUI;
    public Text gameoverScoreText;
    public float timeOut = 5;
    
    private float gameTime = 0;
    private float answerTime = 0;
    private bool isGameEnd = false;
    private bool isInput = false;
    private float score = 0f, timer = 0.1f;
    private int questionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        setQuestion(questionIndex);
        ScoreText.text = "Score: " + score.ToString("0");
        TimerText.text = "Time: " + timer.ToString();
        gamerunningUi.enabled = true;

    }

    private void Update()
    {
        if (!isInput)
        {

            //Debug.Log(gameTime.ToString() + " index of question = "+questionIndex.ToString() + " and timeout = " + timeOut * (questionIndex + 1));

            if (gameTime-answerTime > timeOut && !isGameEnd)
            {
                isGameOver(true);
                isGameEnd = true;
            }
            else
            {
                gameTime = Time.time;
            }

            if (!isGameEnd)
            {
                float displayTime = gameTime - answerTime;
                TimerText.text = displayTime.ToString("0");
            }
        }
    }

    void setQuestion(int index)
    {
        QuestionText.text = questionList[index].question;
        ScoreText.text = "Score: " + score.ToString("0");

        for(int i=0; i<4; i++)
        {
            OptionText[i].text = questionList[index].options[i];
        }

        Invoke("TimeStart", 1f);
    }

    public void getAnswer(int index)
    {

        answerTime = Time.time;
        if (questionList[questionIndex].correctAnsIndex == index)
        {
            Debug.Log("Correct Ans");
            score += questionList[questionIndex].weight * 10;
            isGameOver(false);
        }
        else
        {
            Debug.Log("Wrong Ans");
            isGameOver(true);
        }
    }

    void isGameOver(bool gameOver)
    {
        if(gameOver)
        {
            //Do gameOver Stuff and shit
            gamerunningUi.enabled = false;
            gameFinishUI.SetActive(true);
            gameoverScoreText.text = score.ToString("0");
        }
        else
        {
            if(questionIndex < questionList.Count - 1)
            {
                questionIndex++;
                setQuestion(questionIndex);
            }
            else
            {
                //Game Completed and Stuff
                gamerunningUi.enabled = false;
                gameFinishUI.SetActive(true);
                gameoverScoreText.text = score.ToString("0");
            }

        }
    }

    private void TimeStart()
    {
        gameTime = 0;
        isInput = false;
        TimerText.text = gameTime.ToString();
    }

}
