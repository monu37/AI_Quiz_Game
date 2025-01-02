using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionscript : MonoBehaviour
{
    Image Img;
    [SerializeField] int IndexNo;
    [SerializeField] bool IsCorrectOption;
    [SerializeField] string OptionName;
    Button btn;

    private void Awake()
    {
        Img = GetComponent<Image>();
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => btnclick());
    }

    private void Update()
    {
        //string ans = quizmanager.instance.GetAnswer();

        //checkcorrectoption(ans);
    }

    public void setoptionname(string s)
    {
        OptionName = s;
    }

    public void setindexno(int i)
    {
        IndexNo = i;
    }

    public string GetOptionName()
    {
        return OptionName;
    }

    public void checkcorrectoption(string correctanswer)
    {
        print("Option name:" + OptionName);
        IsCorrectOption = true;
        //if (OptionName == correctanswer)
        //{
        //    print("1111");
        //    IsCorrectOption = true;//
        //}
        //else
        //{
        //    print("22");
        //    IsCorrectOption = false;

        //}
    }

    void btnclick()
    {
        if (IsCorrectOption)
        {
            quizmanager.instance.setoptionclr(IndexNo, true);
            print("you earn 100 coins");

            quizmanager.instance.win();
        }
        else
        {
            quizmanager.instance.setoptionclr(IndexNo, false);
            print("you lose");
            quizmanager.instance.lose();
        }
    }
}
