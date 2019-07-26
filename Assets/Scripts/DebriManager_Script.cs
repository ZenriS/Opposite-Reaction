using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DebriManager_Script : MonoBehaviour
{
    public int Damage;
    public bool Move;
    private Transform _endPoint;
    public float MoveTime;
    
    void Start()
    {
        if (Move)
        {
            _endPoint = transform.GetChild(0);
            this.transform.DOMove(_endPoint.position, MoveTime).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        }
    }
}
