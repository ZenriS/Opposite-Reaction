using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMananger_Script : MonoBehaviour
{
    public Slider SFXSlider;
    public Slider MusicSlider;
    private AudioSource _audioSourceMusic;
    private AudioSource _audioSourceSFX;
    public float SFXVolume;
    public float MusicVolume;

    void Start()
    {
        _audioSourceMusic = GetComponents<AudioSource>()[0];
        _audioSourceSFX = GetComponents<AudioSource>()[1];
        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetFloat("sfx", 0.8f);
            PlayerPrefs.SetFloat("music", 0.2f);
            PlayerPrefs.SetInt("first", 1);
        }
        SFXVolume = PlayerPrefs.GetFloat("sfx");
        MusicVolume = PlayerPrefs.GetFloat("music");
        _audioSourceMusic.volume = MusicVolume;
        MusicSlider.value = MusicVolume;
        _audioSourceSFX.volume = SFXVolume;
        SFXSlider.value = SFXVolume;
    }

    public void UpdateSFXVolume()
    {
        SFXVolume = SFXSlider.value;
        _audioSourceSFX.volume = SFXVolume;
        PlayerPrefs.SetFloat("sfx",SFXVolume);
    }

    public void UpdateMusicVolume()
    {
        MusicVolume = MusicSlider.value;
        _audioSourceMusic.volume = MusicVolume;
        PlayerPrefs.SetFloat("music", MusicVolume);
    }
}
