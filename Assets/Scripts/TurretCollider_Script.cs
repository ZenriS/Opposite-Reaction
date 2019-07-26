using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollider_Script : MonoBehaviour
{
    private Turret_Script _turretScript;

    void Start()
    {
        _turretScript = transform.parent.GetComponent<Turret_Script>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            _turretScript.TargetPlayer(c.transform);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            _turretScript.ClearTarget();
        }
    }
}
