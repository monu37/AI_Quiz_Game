using OpenAI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HuggingFace.API;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class quizmanager : MonoBehaviour
{
    public static quizmanager instance;

    [SerializeField] int QuizNumber;

    [SerializeField] GameObject LobbyBtn;
    [SerializeField] float LobbyYPos;
    [SerializeField] string QuestionPrompt;
    [SerializeField] TextMeshProUGUI QuestionText;
    [SerializeField] Button NextBtn;
    private OpenAIApi openai = new OpenAIApi();

    private List<ChatMessage> messages = new List<ChatMessage>();
    private string prompt = "Answer the question: ";

    [SerializeField] string Question;
    [SerializeField] List<TextMeshProUGUI> OptionsText;
    [SerializeField] string Answer;
    [SerializeField] List<string> OptionsArray;
    [SerializeField] List<Button> OptionBtns;
    [SerializeField] Color NormalOptionColr;
    [SerializeField] Color WrongOptionColr;
    [SerializeField] Color RightOptionColr;
    [Space]
    [Header("Hint")]
    [SerializeField] Button HintBtn;
    [SerializeField] GameObject HintPanel;
    [SerializeField] Image HintImage;
    [SerializeField] TextMeshProUGUI HintAnswerText;

    [Header("Timer")]
    [SerializeField] bool IsGameStart;
    [SerializeField] Image TimerImage;
    [SerializeField] float GameTime;
    float DefaultGameTime;

    [Header("Result Panel")]
    [SerializeField] GameObject LosePanel;
    [SerializeField] Image ResumeImg;

    [SerializeField] GameObject LoadingPanel;

    private void Awake()
    {
        instance = this;

        QuestionPrompt = promptscript.instance.GetPrompt();
        DefaultGameTime = GameTime;
        IsGameStart = false;
    }

    private void Start()
    {
        LobbyBtn.transform.DOLocalMoveY(LobbyYPos, .5f);

        HintImage.gameObject.SetActive(false);
        LoadingPanel.SetActive(true);


        QuizNumber = promptscript.instance.GetQuizNumber();
        normaloptionclr();
        for (int i = 0; i < OptionBtns.Count; i++)
        {
            OptionBtns[i].GetComponent<optionscript>().setindexno(i);
            //assign text
            OptionsText.Add(OptionBtns[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }

        NextBtn.onClick.RemoveAllListeners();
        NextBtn.onClick.AddListener(() => nextquestion());

        SendReply(QuestionPrompt);


    }

    void normaloptionclr()
    {
        for (int i = 0; i < OptionBtns.Count; i++)
        {
            OptionBtns[i].image.color = NormalOptionColr;
        }
    }

    public void setoptionclr(int optionno,bool iswin)
    {
        for (int i = 0; i < OptionBtns.Count; i++)
        {
            if (optionno == i)
            {
                if (iswin)
                {
                    OptionBtns[optionno].image.color = RightOptionColr;
                }
                else
                {
                    OptionBtns[optionno].image.color = WrongOptionColr;
                }
            }
        }
    }

    private void Update()
    {
        if (IsGameStart)
        {
            if (TimerImage.fillAmount >= 0f)
            {
                TimerImage.fillAmount -= Time.deltaTime * GameTime;//
                if (TimerImage.fillAmount <= 0)
                {
                    lose();
                }
            }
        }

        if (LosePanel.activeInHierarchy)
        {
            IsGameStart = false;
            ResumeImg.fillAmount -= Time.deltaTime * .08f;
            if(ResumeImg.fillAmount <= 0)
            {
                ResumeImg.gameObject.SetActive(false);
            }
        }
    }

    private void AppendMessage(ChatMessage message)
    {
        //print("Response " + "\n" + message.Content)/*;*/
    }

    private async void SendReply(string prompt1)
    {
        LoadingPanel.SetActive(true);
        QuestionText.transform.GetComponentInParent<animationmanager>().offanim();
        normaloptionclr();
        for (int i = 0; i < OptionBtns.Count; i++)
        {
            OptionBtns[i].GetComponent<animationmanager>().offanim();
        }
        HintImage.gameObject.SetActive(false);
        //HintBtn.gameObject.SetActive(false);
        HintBtn.GetComponent<animationmanager>().offanim();

        NextBtn.GetComponent<animationmanager>().offanim();
        //TimerImage.gameObject.SetActive(false);
        TimerImage.GetComponent<animationmanager>().offanim();
        TimerImage.fillAmount = 1;

        OptionsArray.Clear();
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = QuestionPrompt
        };

        AppendMessage(newMessage);

        if (messages.Count == 0) newMessage.Content = prompt + prompt1;

        messages.Add(newMessage);

        // Complete the instruction
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-3.5-turbo-0613",
            Messages = messages
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            AppendMessage(message);
            string response = message.Content;

            print("Response : " + response);

            string pattern = @"(Who|What|When|Which|Where|Why|How|Is|Are|Can|Could|Will|Would)";
            Match match = Regex.Match(response, pattern);

            string response1;
            if (match.Success)
            {
                print("question contain : " + match.Value);
                response1 = response.Substring(match.Index);
            }
            else
            {
                print("question is not contain anything : " );
                response1 = "Hello ";
            }

            print("new response : " + response1);

            string[] splitquestion = response1.Split('?');

            if (splitquestion.Length == 2)
            {
                QuestionText.transform.GetComponentInParent<animationmanager>().onanim();

                string question = splitquestion[0].Trim();
                string options = splitquestion[1].Trim();

                print("question :" + question);
                print("options :" + options);

                QuestionText.text = question + "?";
                Question = question;

                string patterns = @"[a-zA-Z]+\)|\d+\)";

                Match matchs = Regex.Match(options, patterns);
                //int indexx = options.IndexOf(@"[a-zA-Z]+\)|\d+\)");
                if (matchs.Success)
                {
                    int startIndex = match.Index;
                    Debug.Log("Starting index of match: " + startIndex);
                }
                else
                {
                    print("failed");
                }


                //print(indexx);
                //string option1 = options.Substring(indexx).Trim();
                string option1 = options.Replace(" ", "");
                print(option1);

                option1 = Regex.Replace(options, @"[a-zA-Z]+\)|\d+\)", "");
                //option1 = Regex.Replace(options, " ", "");

               
                print("new option: " + option1);

                string[] Splitoption = option1.Split('\n');
                foreach (string opt in Splitoption)
                {
                    OptionsArray.Add(opt);
                }

                if (OptionsArray[0].Contains(":"))
                {
                    OptionsArray.RemoveAt(0);
                }

                List<string> alloptions = new List<string>();

                for (int i = 0; i < OptionsArray.Count; i++)
                {
                    if(OptionsArray[i].Contains(" "))
                    {
                        OptionsArray[i].Replace(" ", "");
                    }
                }

                for (int i = 0; i < OptionsText.Count; i++)
                {
                    OptionsText[i].text = OptionsArray[i];
                    alloptions.Add(OptionsText[i].text);
                    OptionBtns[i].GetComponent<optionscript>().setoptionname(OptionsArray[i]);
                    
                }

                for (int i = 0; i < OptionBtns.Count; i++)
                {
                    OptionBtns[i].GetComponent<animationmanager>().onanim();
                }

                string Answer1 = OptionsArray[OptionsArray.Count - 1];
               

                if (Answer1.Contains(":"))
                {
                    int index = Answer1.IndexOf(":");
                    if(index != -1)
                    {
                        Answer = Answer1.Substring(index + 1);
                    }
                }
                else
                {
                    Answer = Answer1;
                }

                for (int i = 0; i < OptionBtns.Count; i++)
                {
                    if (Answer1.Contains(OptionBtns[i].GetComponent<optionscript>().GetOptionName()))
                    {
                        print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                        OptionBtns[i].GetComponent<optionscript>().checkcorrectoption(Answer);
                    }
                }
                LoadingPanel.SetActive(false);
                // game start
               

                TimerImage.gameObject.SetActive(true);
                TimerImage.GetComponent<animationmanager>().onanim();

                Invoke(nameof(startgame), .2f);
            }
            else
            {
                print("Error : " + splitquestion);
               
            }
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

    }

    public void setgamestart(bool b)
    {
        IsGameStart = b;
        Invoke(nameof(showhintbtn), .2f);
    }

    void showhintbtn()
    {
        HintBtn.gameObject.SetActive(true);
        HintBtn.GetComponent<animationmanager>().onanim();
    }

    void startgame()
    {
        setgamestart(true);
    }
  
    public void hintonoff(bool b)
    {
        soundmanager.instance.clicksound();
        if (b)
        {
            IsGameStart = false;
        }
        else
        {
            IsGameStart = true;
        }

        HintPanel.SetActive(b);
    }

    public void showhintimage(int hintconsume,GameObject panel)
    {
        print("hint images");
        int totalhint = helper.GetTotalHint();
        if (totalhint >= hintconsume)
        {
            if (Answer != null)
            {
                //panel.SetActive(true);
                panel.GetComponent<animationmanager>().onanim();

                totalhint -= hintconsume;
                helper.settotalhint(totalhint);

                print("hint images");
                DallE.instance.ShowImage(HintImage, Answer);
            }
        }
        else
        {
            panel.transform.GetComponentInParent<hintpanel>().gameObject.SetActive(false);
            dontdestroy.instance.hintonoff(true);
        }
    }

    public void showhintanswer(int hintcoinconsume,GameObject panel)
    {
        print("hint answer");
        int totalcoin = helper.GetTotalCoin();
        if (totalcoin >= hintcoinconsume)
        {
            panel.GetComponent<animationmanager>().onanim();

            totalcoin -= hintcoinconsume;
            helper.settotalcoin(totalcoin);

            print("hint answer");

            HintAnswerText.text = Answer;
        }
        else
        {
            panel.transform.GetComponentInParent<hintpanel>().gameObject.SetActive(false);
            dontdestroy.instance.shoponoff(true);
        }
    }

    public string GetAnswer()
    {
        return Answer;
    }

    public void lobby()
    {
        soundmanager.instance.clicksound();
        SceneManager.LoadScene("lobby");
    }

    public void openshop()
    {
        dontdestroy.instance.shoponoff(true);
    }
    public void openhint()
    {
        dontdestroy.instance.hintonoff(true);
    }

    public void lose()
    {
        LosePanel.SetActive(true);
        ResumeImg.fillAmount = 1;
    }

    public void resumebtn()
    {
        soundmanager.instance.clicksound();
        int totalcoin = helper.GetTotalCoin();
        int resumeamount = 500;
        if (totalcoin >= resumeamount)
        {
            
            totalcoin -= resumeamount;
            helper.settotalcoin(totalcoin);

            IsGameStart = true;
            TimerImage.fillAmount = TimerImage.fillAmount + .05f;

            normaloptionclr();
            LosePanel.SetActive(false);
        }
        else
        {
            dontdestroy.instance.shoponoff(true);
        }
    }

    public void restart()
    {
        soundmanager.instance.clicksound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void win()
    {
        IsGameStart = false;

        NextBtn.GetComponent<animationmanager>().onanim();

        int level = helper.GetOptionLevel(QuizNumber);

        int winprice = Random.Range(10, 50) * level;
        int totalwin = helper.GetTotalCoin() + winprice;
        helper.settotalcoin(totalwin);
    }

    void nextquestion()
    {
        soundmanager.instance.clicksound();
        int level = helper.GetOptionLevel(QuizNumber);
        helper.setoptionlevel(QuizNumber, level + 1);

        string nextquestion = "next";
        SendReply(nextquestion);

        
    }

}
