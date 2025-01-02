using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shopcoin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinCountText;
    [SerializeField] int CoinCount;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] float PriceCount;

    private void Awake()
    {
        CoinCountText.text = CoinCount.ToString() + " Coins";
        PriceText.text = "$ " + PriceCount.ToString();

        gameObject.name = CoinCount.ToString() + " Coins";
    }

    void clickbtn()
    {

    }
}
