using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelscript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] int QuizNumber;

    [SerializeField] float AnimYPos;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "lobby")
        {

            LevelText = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
       
        if (helper.GetOptionLevel(QuizNumber) == 0)
        {
            helper.setoptionlevel(QuizNumber, 1);
        }

        if (SceneManager.GetActiveScene().name == "quizgame")
        {
            transform.DOLocalMoveY(AnimYPos, .5f);
            QuizNumber = promptscript.instance.GetQuizNumber();
           
        }
    }
    private void Update()
    {
        LevelText.text = "Level: " + helper.GetOptionLevel(QuizNumber).ToString();
    }

}
