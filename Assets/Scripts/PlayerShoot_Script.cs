using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerShoot_Script : MonoBehaviour
{
    //private PlayerControls _playerControls;
    public GameObject BulletPrefab;
    private Transform _gunPivot;
    private Rigidbody2D _playerRB;
    public float _scrollWheel;
    private PlayerWeapon_Script[] _weapons;
    private PlayerWeapon_Script _activeWeapons;
    private int _weaponIndex;
    private int _weaponAmount;
    public TextMeshProUGUI AmmoText;
    private PlayerManager_Script _playerManager;
    private SoundEffectManager_Script _soundEffect;
    public AudioClip[] AudioClips;
    private bool _first;
    private bool _paused;

    void OnEnable()
    {
       // _playerControls.Player.Enable();
       // _playerControls.Meny.Enable();
       PauseManager_Script.OnToggleControls += ToggleControls;
    }

    void OnDisable()
    {
       // _playerControls.Player.Disable();
       // _playerControls.Meny.Disable();
        PauseManager_Script.OnToggleControls -= ToggleControls;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            ChangeWeaponID();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Awake()
    {
        _weapons = new PlayerWeapon_Script[3];
        //_playerControls = new PlayerControls();
        _gunPivot = transform.GetChild(0);
        _playerRB = GetComponent<Rigidbody2D>();
        _weapons[0] = transform.GetChild(0).GetChild(0).GetComponent<PlayerWeapon_Script>();
        _playerManager = GetComponent<PlayerManager_Script>();
        _soundEffect = _playerManager.GameMananger.GetComponent<SoundEffectManager_Script>();
        _weaponAmount = 1;
        ChangeWeapon(0,0);
        //_playerControls.Player.Shoot.performed += ctx => Shoot();
        //_playerControls.Player.ChangeWeapon.performed += ctx => _scrollWheel = ctx.ReadValue<float>();
        //_playerControls.Player.ChangeWeapon.performed += ctx => ChangeWeaponID();
    }

    void ToggleControls()
    {
        /*if (_playerControls.Player.enabled)
        {
            _playerControls.Player.Disable();
        }
        else
        {
            _playerControls.Player.Enable();
        }*/
        _paused = !_paused;
    }

    void Shoot()
    {
        if (_paused)
        {
            return;
        }
        _activeWeapons.Shoot();
    }

    public void UpdateAmmo()
    {
        AmmoText.text = _activeWeapons.Name +"\nAmmo: " + _activeWeapons.Ammo + "/" + _activeWeapons.MaxAmmo;
    }

    void ChangeWeaponID()
    {
        int currentIndex = _weaponIndex;
        if (_scrollWheel > 0)
        {
            _weaponIndex++;
        }
        else if (_scrollWheel < 0)
        {
            _weaponIndex--;
        }
        if (_weaponIndex > _weaponAmount - 1)
        {
            _weaponIndex = 0;
        }
        else if (_weaponIndex < 0)
        {
            _weaponIndex = _weaponAmount - 1;
        }
        _scrollWheel = 0;
        ChangeWeapon(_weaponIndex,currentIndex);
    }

    void ChangeWeapon(int i, int p)
    {
        foreach (PlayerWeapon_Script w in _weapons)
        {
            if (w != null)
            {
                w.gameObject.SetActive(false);
            }
        }
        if (_weapons[i] == null)
        {
            _weaponIndex = p;
            i = p;
        }
        if (_weaponIndex != p)
        {
            _soundEffect.PlayEffectFromExternal(AudioClips[0]);
        }
        _first = true;
        _weapons[i].gameObject.SetActive(true);
        _activeWeapons = _weapons[i];
        UpdateAmmo();
    }

    public void PickUpGun(string id, int a)
    {
        _soundEffect.PlayEffectFromExternal(AudioClips[0]);
        if (id == "pistol")
        {
            if (_weapons[0] == null)
            {
                Debug.Log("Add Pistol");
                _weapons[0] = transform.GetChild(0).GetChild(1).GetComponent<PlayerWeapon_Script>();
            }
            else
            {
                Debug.Log("Add Pistol Ammo");
                _weapons[0].GetAmmo(a);
            }
        }
        else if (id == "sub")
        {
            PlayerWeapon_Script pw = transform.GetChild(0).GetChild(1).GetComponent<PlayerWeapon_Script>();
            if (!pw.PickedUp)
            {
                if (_weapons[1] == null)
                {
                    Debug.Log("Add Sub");
                    _weapons[1] = pw;
                    _weaponAmount++;
                }
                else if (_weapons[2] == null)
                {
                    Debug.Log("Add Sub");
                    _weapons[2] = pw;
                    _weaponAmount++;
                }

                pw.PickedUp = true;
            }
            else
            {
                Debug.Log("Add Sub Ammo");
                _weapons[1].GetAmmo(a);
            }
        }
        else if (id == "shotgun")
        {
            PlayerWeapon_Script pw = transform.GetChild(0).GetChild(2).GetComponent<PlayerWeapon_Script>();
            if (!pw.PickedUp)
            {
                if (_weapons[1] == null)
                {
                    Debug.Log("Add Shotgun");
                    _weapons[1] = pw;
                    _weaponAmount++;
                }
                else if (_weapons[2] == null)
                {
                    Debug.Log("Add Shotgun");
                    _weapons[2] = pw;
                    _weaponAmount++;
                }

                pw.PickedUp = true;
            }
            else
            {
                Debug.Log("Add Shotgun Ammo");
                _weapons[1].GetAmmo(a);
            }
        }
    }
}
