using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPick_Script : MonoBehaviour
{
    public int Healing;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            Debug.Log(c.transform.name);
            Debug.Log("Healing player");
            PlayerHealth_Script ph = c.transform.GetComponent<PlayerHealth_Script>();
            if (ph.ActiveHealth < ph.MaxHealth)
            {
                ph.Heal(Healing);
                Destroy(this.gameObject);
            }
        }
    }
}
