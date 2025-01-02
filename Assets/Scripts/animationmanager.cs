using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class animationmanager : MonoBehaviour
{
    public static animationmanager instance;
   
    [SerializeField] AnimType Anim;
    [SerializeField] float DefaultMoveValue;
    [SerializeField] float MoveValue;
    [SerializeField] float MoveSpeed;
    [SerializeField] bool CheckOnAnim;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (CheckOnAnim)
        {
            onanim();
        }
        else
        {
            offanim();  
        }
    }
    public void onanim()
    {
        switch (Anim)
        {
            case AnimType.MoveX:
                transform.DOLocalMoveX(MoveValue, MoveSpeed);

                break;

            case AnimType.MoveY:
                transform.DOLocalMoveY(MoveValue, MoveSpeed);

                break;

            case AnimType.Scale:
                transform.DOScale(Vector3.one, MoveSpeed);

                break;
        }
    }
    
    public void offanim()
    {
        switch (Anim)
        {
            case AnimType.MoveX:
                transform.DOLocalMoveX(DefaultMoveValue, MoveSpeed);

                break;

            case AnimType.MoveY:
                transform.DOLocalMoveY(DefaultMoveValue, MoveSpeed);

                break;

            case AnimType.Scale:
                transform.DOScale(Vector3.zero, MoveSpeed);

                break;
        }
    }
}

[System.Serializable]

public enum AnimType
{
    MoveX, MoveY, Scale
}


