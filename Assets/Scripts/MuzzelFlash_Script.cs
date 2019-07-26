using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MuzzelFlash_Script : MonoBehaviour
{
    public Vector3 FlashSize;
    public void Flash()
    {
        //transform.DOComplete();
        transform.DOPunchScale(FlashSize, 0.1f, 10, 1f);
    }
}
