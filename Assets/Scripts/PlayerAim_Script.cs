using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerAim_Script : MonoBehaviour
{
    //private PlayerControls _playerControls;
    private Transform _gunPivot;
    private Vector2 _mousePos;
    private bool _paused;

    void OnEnable()
    {
        //_playerControls.Player.Enable();
        //_playerControls.Meny.Enable();
        PauseManager_Script.OnToggleControls += ToggleControls;
    }

    void OnDisable()
    {
        //_playerControls.Player.Disable();
        //_playerControls.Meny.Disable();
        PauseManager_Script.OnToggleControls -= ToggleControls;
    }

    void Awake()
    {
        //_playerControls = new PlayerControls();
        _gunPivot = transform.GetChild(0);
        //_playerControls.Player.Aim.performed += ctx => _mousePos = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        Aim();
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

    void Aim()
    {
        if (_paused)
        {
            return;
        }
        _mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 direction = new Vector2(screenPos.x - _gunPivot.position.x, screenPos.y - _gunPivot.position.y);
        _gunPivot.transform.up = direction;
    }

}
