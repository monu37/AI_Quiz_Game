using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hintscrpt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HintCountText;
    [SerializeField] float AnimYPos;



    private void Start()
    {
        transform.DOLocalMoveY(AnimYPos, .5f);
    }

    private void Update()
    {
        HintCountText.text = helper.GetTotalHint().ToString();
    }
}
