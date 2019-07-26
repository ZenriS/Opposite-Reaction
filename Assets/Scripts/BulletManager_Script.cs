using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager_Script : MonoBehaviour
{
    public int Damage;
    private float _range;
    //private Transform _startPos;
    private Vector3 _startPos;
    public bool PlayerBullet;
    private SoundMananger_Script _soundMananger;
    public AudioClip AudioClip;
    private AudioSource _audioSource;
    private CircleCollider2D _circleCollider2D;
    private bool _runOnce;

    void Start()
    {
        _soundMananger = FindObjectOfType<SoundMananger_Script>();
        _audioSource = GetComponent<AudioSource>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        CheckLocation();
    }

    void CheckLocation()
    {
        float distance = Vector3.Distance(_startPos, this.transform.position);
        if (distance >= _range)
        {
            Destroy();
        }
    }

    /*void OnTriggerEnter2D(Collider2D c)
    {
        if (PlayerBullet)
        {
            if (c.tag != "Player" && c.tag != "turretCol")
            {
                Debug.Log("Player: " + PlayerBullet + " Hit: " + c.transform.name);
                Destroy();
            }
        }
        else if (!PlayerBullet)
        {
            if (c.tag != "turret" && c.tag != "turretCol")
            {
                Debug.Log("Player: " + PlayerBullet + " Hit: " + c.transform.name);
                Destroy();
            }
        }
    }*/

    void OnCollisionEnter2D(Collision2D c)
    {
        if (PlayerBullet)
        {
            if (c.transform.tag != "Player" && c.transform.tag != "turretCol" && c.transform.tag != "pbullet")
            {
                //Debug.Log("Player: " + PlayerBullet + " Hit: " + c.transform.name);
                Destroy();
            }
        }
        else if (!PlayerBullet)
        {
            if (c.transform.tag != "turret" && c.transform.tag != "turretCol")
            {
                //Debug.Log("Player: " + PlayerBullet + " Hit: " + c.transform.name);
                Destroy();
            }
        }
    }

    void Destroy()
    {
        if (!_runOnce)
        {
            float p = Random.Range(0.8f, 1.2f);
            _audioSource.pitch = p;
            _audioSource.PlayOneShot(AudioClip, _soundMananger.SFXVolume);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            _circleCollider2D.enabled = false;
            Destroy(this.gameObject, 0.5f);
            _runOnce = true;
        }

    }

    public void Setup(int d, float r, Transform s)
    {
        Damage = d;
        _range = r;
        _startPos = s.position;
    }

}
