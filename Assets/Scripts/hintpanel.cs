using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hintpanel : MonoBehaviour
{
    [SerializeField] GameObject HintPanel;
    [SerializeField] Button ImageHintBtn, AnswerHintBtn;
    [SerializeField] int ImageHintPrice, AnswerHintPrice;
    [SerializeField] TextMeshProUGUI ImageHintPriceText, AnswerHintPriceText;
    [SerializeField] GameObject HintImagePanel;
    [SerializeField] GameObject HintAnswerPanel;

    private void OnEnable()
    {
        //HintPanel.SetActive(true);
        HintPanel.GetComponent<animationmanager>().onanim();
        HintImagePanel.GetComponent<animationmanager>().offanim();
        HintAnswerPanel.GetComponent<animationmanager>().offanim();
    }

    private void OnDisable()
    {
        HintPanel.GetComponent<animationmanager>().offanim();
        HintImagePanel.GetComponent<animationmanager>().offanim();
        HintAnswerPanel.GetComponent<animationmanager>().offanim();
    }

    private void Start()
    {
        ImageHintPriceText.text = ImageHintPrice.ToString();
        AnswerHintPriceText.text = AnswerHintPrice.ToString();

        ImageHintBtn.onClick.RemoveAllListeners();
        ImageHintBtn.onClick.AddListener(() => imagehint());

        AnswerHintBtn.onClick.RemoveAllListeners();
        AnswerHintBtn.onClick.AddListener(() => answerhint());
    }

    void imagehint()
    {
        soundmanager.instance.clicksound();
        print("image");
        quizmanager.instance.showhintimage(ImageHintPrice, HintImagePanel);
        //HintAnswerPanel.GetComponent<animationmanager>().offanim();
        //HintAnswerPanel.SetActive(false);
    }

    void answerhint()
    {
        soundmanager.instance.clicksound();
        print("answer");
        quizmanager.instance.showhintanswer(AnswerHintPrice, HintAnswerPanel);
        //HintImagePanel.SetActive(false);
        //HintImagePanel.GetComponent<animationmanager>().offanim();
    }
}
