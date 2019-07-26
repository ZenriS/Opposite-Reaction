using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInn_Script : MonoBehaviour
{
    private Image _spriteRen;
    public float FadeTime;
    public float FadeValue;

    void Start()
    {
        _spriteRen = GetComponent<Image>();
        _spriteRen.enabled = true;
        StartCoroutine(FadeTo(FadeValue, FadeTime));
    }


    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = _spriteRen.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            _spriteRen.color = newColor;
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
