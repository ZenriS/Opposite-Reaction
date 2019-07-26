using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_Script : MonoBehaviour
{
    public CamFollow_Script CamFollow;
    public GameObject GameMananger;
    public List<string> KeyID;
    public GameObject KeyIconsHolder;
    private GameObject[] _keyIcons;
    private Rigidbody2D _playerRB;
    private float _localScaleX;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        SetUpUI();
        _localScaleX = transform.localScale.x;
    }

    void Update()
    {
        CheckDirection();
    }

    void CheckDirection()
    {
        if (_playerRB.velocity.x < 0)
        {
            //transform.localScale = new Vector3(_localScaleX, 1, 1);
            _spriteRenderer.flipX = true;
        }
        else
        {
            //transform.localScale = new Vector3(-_localScaleX, 1, 1);
            _spriteRenderer.flipX = false;
        }
    }

    void SetUpUI()
    {
        _keyIcons = new GameObject[3];
        _keyIcons[0] = KeyIconsHolder.transform.GetChild(0).gameObject;
        _keyIcons[1] = KeyIconsHolder.transform.GetChild(1).gameObject;
        _keyIcons[2] = KeyIconsHolder.transform.GetChild(2).gameObject;
        foreach (GameObject k in _keyIcons)
        {
            k.SetActive(false);
        }
    }

    public void AddKey(string id)
    {
        if (!KeyID.Contains(id))
        {
            KeyID.Add(id);
            UpdateKeyUI();
        }
    }

    void UpdateKeyUI()
    {
        if (KeyID.Contains("red"))
        {
            _keyIcons[0].SetActive(true);
        }
        if (KeyID.Contains("blue"))
        {
            _keyIcons[1].SetActive(true);
        }
        if (KeyID.Contains("green"))
        {
            _keyIcons[2].SetActive(true);
        }
    }

    public bool CheckKey(string i)
    {
        bool c = false;
        foreach (string id in KeyID)
        {
            if (i == id)
            {
                c = true;
                break;
            }
        }
        return c;
    }
}
