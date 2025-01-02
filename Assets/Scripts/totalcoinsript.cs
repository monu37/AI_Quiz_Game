using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class totalcoinsript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] float AnimYPos;

   

    private void Start()
    {
        transform.DOLocalMoveY(AnimYPos, .5f);
    }

    private void Update()
    {
        CoinText.text = helper.GetTotalCoin().ToString();
    }
}
