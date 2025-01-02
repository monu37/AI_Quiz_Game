using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontdestroy : MonoBehaviour
{
    public static dontdestroy instance;

    [SerializeField] GameObject ShopPanel;
    [SerializeField] GameObject HintPanel;

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


    public void shoponoff(bool b)
    {
        soundmanager.instance.clicksound();
        if (SceneManager.GetActiveScene().name == "quizgame")
        {
            quizmanager.instance.setgamestart(!b);
        }
       
        if (b)
        {
            ShopPanel.SetActive(b);
            ShopPanel.transform.GetChild(0).DOScale(Vector3.one, .3f);
        }
        else
        {
            ShopPanel.transform.GetChild(0).DOScale(Vector3.zero, .3f).OnComplete(() =>
            {
                ShopPanel.SetActive(false);
            });
        }
    }

    public void hintonoff(bool b)
    {
        soundmanager.instance.clicksound();
        if (SceneManager.GetActiveScene().name == "quizgame")
        {
            quizmanager.instance.setgamestart(!b);
        }
        if (b)
        {
            HintPanel.SetActive(true);
            HintPanel.transform.GetChild(0).DOScale(Vector3.one, .3f);
        }
        else
        {
            HintPanel.transform.GetChild(0).DOScale(Vector3.zero, .3f).OnComplete(() =>
            {
                HintPanel.SetActive(false);
            });
        }
    }
}
