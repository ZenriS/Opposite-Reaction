using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon_Script : MonoBehaviour
{
    public bool PickedUp;
    public string Name;
    public GameObject BulletPrefab;
    public int Ammo;
    public int MaxAmmo;
    public bool InfiniteAmmo;
    public float Kickback;
    public float Cooldown;
    public bool Spread;
    public bool Burst;
    public int BulletDamage;
    public float BulletRange;
    public float CamShakeAmount;
    private List<Transform> _bulletExit;
    private bool _allowShoot;
    private Rigidbody2D _playerRB;
    private PlayerShoot_Script _playerShoot;
    private PlayerManager_Script _playerManager;
    private CamFollow_Script _camFollow;
    private MuzzelFlash_Script _muzzelFlash;
    public AudioClip[] AudioClips;
    private SoundEffectManager_Script _effectManager;

    void Start()
    {
        _bulletExit = new List<Transform>();
        _bulletExit.Add(transform.GetChild(1));
        if (Spread)
        {
            _bulletExit.Add(transform.GetChild(2));
            _bulletExit.Add(transform.GetChild(3));
        }
        _playerRB = transform.parent.parent.GetComponent<Rigidbody2D>();
        _playerShoot = _playerRB.transform.GetComponent<PlayerShoot_Script>();
        _playerManager = _playerRB.transform.GetComponent<PlayerManager_Script>();
        _muzzelFlash = GetComponentInChildren<MuzzelFlash_Script>();
        _effectManager = _playerManager.GameMananger.GetComponent<SoundEffectManager_Script>();
        _camFollow = _playerManager.CamFollow;
        _allowShoot = true;
    }

    public void Shoot()
    {
        if (Ammo > 0)
        {
            //NB: Need to get spawnlocation from gun, to match bullet to muzzel. probly move shoot to run for each gun, since behavior is different
            if (Burst)
            {
                Invoke("SpawnBullet", 0);
                Invoke("SpawnBullet", 0.1f);
                Invoke("SpawnBullet", 0.2f);
                Invoke("DisallowShoot", 0.21f);
            }
            else if (Spread)
            {
                SpreadShoot();
                _allowShoot = false;
            }
            else
            {
                Invoke("SpawnBullet", 0);
                Invoke("DisallowShoot", 0.01f);
            }
            Invoke("AllowShoot", Cooldown);
        }
        else
        {
            //Empty sounds effect
            Debug.Log("No ammo");
        }
    }

    void SpawnBullet()
    {
        if (_allowShoot && Ammo > 0)
        {
            GameObject b = Instantiate(BulletPrefab, _bulletExit[0].position, Quaternion.identity);
            Rigidbody2D br = b.GetComponent<Rigidbody2D>();
            BulletManager_Script bm = b.GetComponent<BulletManager_Script>();
            bm.Setup(BulletDamage,BulletRange,this.transform);
            br.AddForce(_bulletExit[0].right * 1000);
            _playerRB.AddForce(-_bulletExit[0].right * Kickback);
            if (!InfiniteAmmo)
            {
                Ammo--;
                _playerShoot.UpdateAmmo();
            }
            _effectManager.PlayEffectFromExternal(AudioClips[0]);
            _camFollow.CamShake(CamShakeAmount);
            _muzzelFlash.Flash();
        }
    }

    void SpreadShoot()
    {
        if (_allowShoot && Ammo > 0)
        {
            _playerRB.AddForce(-_bulletExit[0].right * Kickback);
            for (int i = 0; i <= 2; i++)
            {
                GameObject b = Instantiate(BulletPrefab, _bulletExit[0].position, Quaternion.identity);
                Rigidbody2D br = b.GetComponent<Rigidbody2D>();
                BulletManager_Script bm = b.GetComponent<BulletManager_Script>();
                bm.Setup(BulletDamage, BulletRange, this.transform);
                br.AddForce(_bulletExit[i].right * 1000);
            }
            if (!InfiniteAmmo)
            {
                Ammo--;
                _playerShoot.UpdateAmmo();
            }
            _effectManager.PlayEffectFromExternal(AudioClips[0]);
            _camFollow.CamShake(CamShakeAmount);
            _muzzelFlash.Flash();
        }
    }

    void AllowShoot()
    {
        _allowShoot = true;
    }

    void DisallowShoot()
    {
        _allowShoot = false;
    }

    public void GetAmmo(int a)
    {
        Ammo += a;
        if (Ammo > MaxAmmo)
        {
            Ammo = MaxAmmo;
        }
        _playerShoot.UpdateAmmo();
        _effectManager.PlayEffectFromExternal(AudioClips[1]);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "wall")
        {
            _playerRB.AddForce(-this.transform.right * 100);
        }
    }
}
