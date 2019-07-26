using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth_Script : MonoBehaviour
{
    public int MaxHealth;
    public int ActiveHealth;
    private SpriteRenderer _spriteRendere;
    private bool _allowDMG;
    private Rigidbody2D _playerRB;
    public Image Healthbar;
    private PlayerManager_Script _playerManager;
    private GameManager_Script _gameManager;
    private SoundEffectManager_Script _soundEffect;
    public AudioClip[] AudioClips;

    void Start()
    {
        _spriteRendere = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerManager = GetComponent<PlayerManager_Script>();
        _gameManager = _playerManager.GameMananger.GetComponent<GameManager_Script>();
        _soundEffect = _gameManager.GetComponent<SoundEffectManager_Script>();
        ActiveHealth = MaxHealth;
        _allowDMG = true;
    }

    void CheckHealth()
    {
        float h = (float)ActiveHealth / (float)MaxHealth;
        Healthbar.fillAmount = h; 
        if (ActiveHealth <= 0)
        {
            ActiveHealth = 0;
            _gameManager.GameOver();
            Destroy(this.gameObject);
            Debug.Log("Player Dead");
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (_allowDMG)
        {
            if (c.transform.tag == "obstacle")
            {
                DebriManager_Script d = c.transform.GetComponent<DebriManager_Script>();
                TakeDamage(d.Damage,true);
            }
            if (c.transform.tag == "ebullet")
            {
                BulletManager_Script bm = c.transform.GetComponent<BulletManager_Script>();
                TakeDamage(bm.Damage,false);
            }
        }
    }

    void TakeDamage(int d, bool i)
    {
        if (i)
        {
            ActiveHealth -= Mathf.RoundToInt(d * calcMultiplier());
        }
        else
        {
            ActiveHealth -= d;
        }
        _soundEffect.PlayEffectFromExternal(AudioClips[0]);
        CheckHealth();
        StartCoroutine("InvisFrames");
    }

    public void Heal(int a)
    {
        ActiveHealth += a;
        if (ActiveHealth > MaxHealth)
        {
            ActiveHealth = MaxHealth;
        }
        _soundEffect.PlayEffectFromExternal(AudioClips[1]);
        CheckHealth();
    }

    IEnumerator InvisFrames()
    {
        _allowDMG = false;
        _spriteRendere.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _spriteRendere.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _spriteRendere.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _spriteRendere.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _spriteRendere.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _spriteRendere.enabled = true;
        _allowDMG = true;
    }

    float calcMultiplier()
    {
        float m = 0;
        float x;
        float y;
        x = _playerRB.velocity.x;
        y = _playerRB.velocity.y;
        if (x < 0)
        {
            x = -x;
        }
        if (y < 0)
        {
            y = -y;
        }
        m = x + y;
        return m;
    }
}
