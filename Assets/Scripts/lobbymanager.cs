using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lobbymanager : MonoBehaviour
{
    public static lobbymanager instance;
    [SerializeField] string PromptString;

    [Space]
    [Header("Options")]
    [SerializeField] GameObject OptionContent;
    [SerializeField] GameObject Option1;
    [SerializeField] GameObject Option2;
    [SerializeField] GameObject Option3;
    [SerializeField] GameObject Option4;
    [SerializeField] float Option1MoveX;
    [SerializeField] float Option2MoveX;
    [SerializeField] float Option3MoveX;
    [SerializeField] float Option4MoveX;
    
    private void Awake()
    {
        instance = this;

        OptionContent.GetComponent<ContentSizeFitter>().enabled = false; 
        OptionContent.GetComponent<HorizontalLayoutGroup>().enabled = false;

        if (helper.GetFirstTime() == 0)//first time game is loading
        {
            helper.settotalhint(5);
            helper.settotalcoin(1000);
            helper.setfirsttime(100);
        }
    }


    private void Start()
    {
        showoptionanim();
    }
    void showoptionanim()
    {
        Option1.transform.DOLocalMoveX(Option1MoveX, .3f).OnComplete(() =>
        {
            Option2.transform.DOLocalMoveX(Option2MoveX, .2f).OnComplete(() => 
            {
                Option3.transform.DOLocalMoveX(Option3MoveX, .2f).OnComplete(() =>
                {
                    Option4.transform.DOLocalMoveX(Option4MoveX, .2f).OnComplete(() =>
                    {
                        OptionContent.GetComponent<ContentSizeFitter>().enabled = true;
                        OptionContent.GetComponent<HorizontalLayoutGroup>().enabled = true;
                    });
                });
            });

        });
    }

    public void loadgame(int i, string ageno)
    {
        setchatgptprompt(i, ageno);
    }

    void setchatgptprompt(int i,string ageno)
    {
        setprompt(ageno);

        promptscript.instance.setprompt(PromptString);
        promptscript.instance.setquiznumber(i);

        Invoke(nameof(loadquizgame), .2f);
    }

    void loadquizgame()
    {
        SceneManager.LoadScene("quizgame");
    }

    public void setprompt(string ageno)
    {
        PromptString = "one quiz question to " + ageno + " year children with 4option and at the end write the answer";
    }

    public void openshop()
    {
        soundmanager.instance.clicksound();
        dontdestroy.instance.shoponoff(true);
    }

    public void hintonoff()
    {
        soundmanager.instance.clicksound();
        dontdestroy.instance.hintonoff(true);
    }

    public void exitgame()
    {
        soundmanager.instance.clicksound();
        Application.Quit();
    }
}
