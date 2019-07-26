using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Turret_Script : MonoBehaviour
{
    public bool Move;
    public float MoveTime;
    public float AimSpeed;
    public int Damage;
    public float Health;
    private float _maxHealth;
    public float BulletRange;
    public float BulletSpeed;
    public float ShootCooldown;
    public GameObject BulletPrefab;
    private Transform _target;
    private Transform _bulletExit;
    private Transform _turretPivot;
    private Ray2D _ray2D;
    public LayerMask RayMask;
    private bool _shootCooldown;
    private Image _healthBar;
    private Vector3 _endPoint;
    private SoundMananger_Script _soundMananger;
    public AudioClip[] AudioClips;
    private AudioSource _audioSource;
    private MuzzelFlash_Script _muzzelFlash;
    private MuzzelFlash_Script _explosion;

    void Start()
    {
        _bulletExit = transform.GetChild(1).GetChild(0).GetChild(0).transform;
        _turretPivot = transform.GetChild(1);
        _healthBar = transform.GetChild(3).GetChild(1).GetComponent<Image>();
        _maxHealth = Health;
        _endPoint = transform.GetChild(4).position;
        _soundMananger = FindObjectOfType<SoundMananger_Script>();
        _audioSource = GetComponent<AudioSource>();
        _muzzelFlash = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<MuzzelFlash_Script>();
        _explosion = transform.GetChild(5).GetComponent<MuzzelFlash_Script>();
        ScanArea();
        StartMove();
    }

    void Update()
    {
        if (_target != null)
        {
            AimAtPlayer();
            RaycastCheck();
        }
    }

    void RaycastCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(_bulletExit.position, -_bulletExit.up, 10f,RayMask);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Player")
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (!_shootCooldown)
        {
            Debug.Log(this.transform + " Shoot");
            GameObject b = Instantiate(BulletPrefab, _bulletExit.position, Quaternion.identity);
            BulletManager_Script bm = b.GetComponent<BulletManager_Script>();
            bm.Setup(Damage, BulletRange, this.transform);
            Rigidbody2D br = b.GetComponent<Rigidbody2D>();
            br.AddForce(-_bulletExit.up * BulletSpeed);
            _shootCooldown = true;
            Invoke("RestShoot", ShootCooldown);
            float p = Random.Range(0.8f, 1.2f);
            _audioSource.pitch = p;
            _audioSource.PlayOneShot(AudioClips[0], _soundMananger.SFXVolume);
            _muzzelFlash.Flash();
        }
    }

    void RestShoot()
    {
        _shootCooldown = false;
    }

    void AimAtPlayer()
    {
        /*   Vector2 direction = new Vector2(_target.position.x - _turretPivot.position.x, _target.position.y - _turretPivot.position.y);
        _turretPivot.transform.up = direction;*/
        Vector3 vectorToTarget = _target.position - _turretPivot.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        _turretPivot.rotation = Quaternion.Slerp(_turretPivot.rotation, q, Time.deltaTime * AimSpeed);
    }

    void ScanArea()
    {
        _turretPivot.DORotate(new Vector3(0, 0, 0), 20, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    void StartMove()
    {
        if (Move)
        {
            this.transform.DOMove(_endPoint, MoveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

   /* void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "pbullet")
        {
            BulletManager_Script b = c.GetComponent<BulletManager_Script>();
            TakeDamage(b.Damage);
            Destroy(c.gameObject);
        }
    }*/

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag == "pbullet")
        {
            BulletManager_Script b = c.transform.GetComponent<BulletManager_Script>();
            TakeDamage(b.Damage);
            Destroy(c.gameObject);
        }
    }

    public void TargetPlayer(Transform p)
    {
        _target = p;
        _turretPivot.DOKill(_turretPivot);
        this.transform.DOPause();
    }

    public void ClearTarget()
    {
        ScanArea();
        this.transform.DOPlay();
        Debug.Log("Player left");
        _target = null;
    }

    void TakeDamage(float d)
    {
        Health -= d;
        float p = Random.Range(0.8f, 1.2f);
        _audioSource.pitch = p;
        if (Health <= 0)
        {
            _audioSource.PlayOneShot(AudioClips[2], _soundMananger.SFXVolume);
            _explosion.Flash();
            Destroy(this.gameObject,0.2f);
        }
        else
        {
            _audioSource.PlayOneShot(AudioClips[1], _soundMananger.SFXVolume);
        }
        _healthBar.fillAmount = Health / _maxHealth;
    }
}
