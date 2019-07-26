using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayCardPickUp_Script : MonoBehaviour
{
    public string KeyID;
    public AudioClip AudioClip;
    
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerManager_Script pm = c.transform.GetComponent<PlayerManager_Script>();
            SoundEffectManager_Script se = pm.GameMananger.GetComponent<SoundEffectManager_Script>();
            se.PlayEffectFromExternal(AudioClip);
            pm.AddKey(KeyID);
            Destroy(this.gameObject);
        }
    }
}
