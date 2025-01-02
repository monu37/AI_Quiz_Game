using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shophint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HintCountText;
    [SerializeField] int HintCount;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] int PriceCount;
    [SerializeField] Button Btn;

    private void Awake()
    {
        HintCountText.text = HintCount.ToString() + " Hints";
        PriceText.text = PriceCount.ToString();

        gameObject.name = HintCount.ToString() + " Hint";

        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() => buyhint());
    }

    void buyhint()
    {
        soundmanager.instance.clicksound();
        int totalcoin = helper.GetTotalCoin();
        int totalhint = helper.GetTotalHint();

        if (totalcoin >= PriceCount)
        {
            totalcoin -= PriceCount;
            helper.settotalcoin(totalcoin);

            totalhint += HintCount;
            helper.settotalhint(totalhint);
        }
        else
        {
            dontdestroy.instance.hintonoff(false);
            dontdestroy.instance.shoponoff(true);
            print("Insufficient coins");
        }
    }
}
