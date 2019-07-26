using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp_Script : MonoBehaviour
{
    public string GunID;
    public int Ammo;
    
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerShoot_Script ps = c.transform.GetComponent<PlayerShoot_Script>();
            ps.PickUpGun(GunID,Ammo);
            Destroy(this.gameObject);
        }
    }
}
