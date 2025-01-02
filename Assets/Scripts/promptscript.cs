using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class promptscript : MonoBehaviour
{
    public static promptscript instance;
    [SerializeField] int QuizNumber;
    [SerializeField] string ChatGptPrompt;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetPrompt()
    {
        return ChatGptPrompt;
    }

    public void setprompt(string prompt)
    {
        ChatGptPrompt = prompt;
    }

    //quiz number
    public void setquiznumber(int quizno)
    {
        QuizNumber = quizno;
    }

    public int GetQuizNumber()
    {
        return QuizNumber;
    }
}
