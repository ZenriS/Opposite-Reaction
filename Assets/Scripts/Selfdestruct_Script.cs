using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct_Script : MonoBehaviour
{
    public float Timer;
    

    void Start()
    {
      Destroy(this.gameObject,Timer);  
    }
}
