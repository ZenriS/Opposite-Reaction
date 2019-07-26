using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager_Script : MonoBehaviour
{
    //private PlayerControls _playerControls;
    private GameObject _playerUI;
    public GameObject PauseUI;
    public bool Paused;
    public delegate void ToggleControlsDel();
    public static event ToggleControlsDel OnToggleControls;

    void OnEnable()
    {
        //_playerControls.Meny.Enable();
    }

    void OnDisable()
    {
        //_playerControls.Meny.Disable();
    }

    void Awake()
    {
        _playerUI = PauseUI.transform.parent.GetChild(0).gameObject;
        //_playerControls = new PlayerControls();
        //_playerControls.Meny.TogglePause.performed += ctx => TogglePause();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        ToggleControls();
        if (!PauseUI.activeInHierarchy)
        {
            PauseUI.SetActive(true);
            _playerUI.SetActive(false);
            Paused = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseUI.SetActive(false);
            _playerUI.SetActive(true);
            Paused = false;
            Time.timeScale = 1;

        }
    }

    public void ToggleControls()
    {
        OnToggleControls();
    }
}
