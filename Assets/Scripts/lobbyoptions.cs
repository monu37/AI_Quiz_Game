using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lobbyoptions : MonoBehaviour
{
    [SerializeField] string AgeNo;
    [SerializeField] int QuizNo;
    [SerializeField] int UnlockPrice;
    [SerializeField] GameObject LockObj, UnlockObj;
    [SerializeField] TextMeshProUGUI UnlockAtText;
    Button btn;
    int TotalCoin;
    bool IsLocked;
    private void Awake()
    {
        IsLocked = true;

        btn = GetComponent<Button>();

        if (QuizNo == 0)
        {
            helper.setoptionlockunlock(QuizNo, 1);
        }
    }
    private void Start()
    {
        if (helper.GetOptionLockUnlock(QuizNo) == 0)//lock
        {
            LockObj.SetActive(true);
            UnlockObj.SetActive(false);
            UnlockAtText.text = "Unlock by using " + "\n" + UnlockPrice + " coin";
            IsLocked = true;
        }
        else //unlock
        {
            LockObj.SetActive(false);
            UnlockObj.SetActive(true);
            IsLocked = false;
        }

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => btnclick());
    }

    public void btnclick()
    {
        soundmanager.instance.clicksound();
        if (IsLocked)//quiz locked
        {
            TotalCoin = helper.GetTotalCoin();
            if (TotalCoin >= UnlockPrice)
            {
                TotalCoin -= UnlockPrice;
                helper.settotalcoin(TotalCoin);

                helper.setoptionlockunlock(QuizNo, 1);
                LockObj.SetActive(false);
                UnlockObj.SetActive(true);
                IsLocked = false;
            }
            else
            {
                dontdestroy.instance.shoponoff(true);
                print("Insufficient coins");
            }
        }
        else //quiz unlock
        {
            print("Game Load");
            lobbymanager.instance.loadgame(QuizNo, AgeNo);
        }
    }


}
