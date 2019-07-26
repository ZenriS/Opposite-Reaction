using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger_Script : MonoBehaviour
{
    public GameManager_Script GameManagerScript;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            GameManagerScript.LevelEnd();
        }
    }
}
