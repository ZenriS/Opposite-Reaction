using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup_Script : MonoBehaviour
{
    public int Amount;
    public int GunIndex;
    public Sprite[] Icons;
    private SpriteRenderer _spRenderer;

    void Start()
    {
        _spRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spRenderer.sprite = Icons[GunIndex];
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerWeapon_Script pw = c.transform.GetChild(0).GetChild(GunIndex).GetComponent<PlayerWeapon_Script>();
            if (pw.Ammo < pw.MaxAmmo)
            {
                pw.GetAmmo(Amount);
                Destroy(this.gameObject);
            }
        }
    }


}
