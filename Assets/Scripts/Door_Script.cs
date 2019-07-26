using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door_Script : MonoBehaviour
{
    public bool Locked;
    public string KeyID;
    public float OpenTime;
    private Transform _mainDoor;
    private Vector3 _endLocation;
    private SpriteRenderer _keyIndicator;
    private SoundMananger_Script _soundMananger;
    public AudioClip[] AudioClips;
    private AudioSource _audioSource;

    void Start()
    {
        _mainDoor = transform.GetChild(0);
        _endLocation = transform.GetChild(1).localPosition;
        _keyIndicator = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        SetKeyIndicator();
    }

    void SetKeyIndicator()
    {
        if (Locked)
        {
            if (KeyID == "red")
            {
                _keyIndicator.color = Color.red;
            }
            else if (KeyID == "blue")
            {
                _keyIndicator.color = Color.blue;
            }
            else if (KeyID == "green")
            {
                _keyIndicator.color = Color.green;
            }
        }
        else
        {
            _keyIndicator.color = Color.white;
        }
        if (KeyID == "")
        {
            _keyIndicator.color = Color.white;
            Locked = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerManager_Script pm = c.GetComponent<PlayerManager_Script>();
            if (_soundMananger == null)
            {
                _soundMananger = pm.GameMananger.GetComponent<SoundMananger_Script>();
            }

            float p = Random.Range(0.8f, 1.2f);
            if (Locked)
            {
  
                if (pm.CheckKey(KeyID))
                {
                    _mainDoor.transform.DOLocalMoveY(_endLocation.y, OpenTime).SetEase(Ease.Linear);

                    _audioSource.PlayOneShot(AudioClips[0],_soundMananger.SFXVolume);
                }
                else
                {

                    _audioSource.PlayOneShot(AudioClips[1], _soundMananger.SFXVolume);
                    StartCoroutine("FlashIndicator");
                }
            }
            else
            {
                _mainDoor.transform.DOLocalMoveY(_endLocation.y, OpenTime).SetEase(Ease.Linear);
                _audioSource.PlayOneShot(AudioClips[0], _soundMananger.SFXVolume);
            }
        }
    }

    IEnumerator FlashIndicator()
    {
        _keyIndicator.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _keyIndicator.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _keyIndicator.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _keyIndicator.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _keyIndicator.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _keyIndicator.enabled = true;
        yield return new WaitForSeconds(0.2f);
    }
}
