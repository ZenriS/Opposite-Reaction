using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager_Script : MonoBehaviour
{
    public AudioClip[] AudioClips;
    private AudioSource _sfxAdAudioSource;

    
    void Start()
    {
        _sfxAdAudioSource = GetComponents<AudioSource>()[1];
    }

    void RandomPitch()
    {
        float p = Random.Range(0.8f, 1.2f);
        _sfxAdAudioSource.pitch = p;
    }

    public void PlayEffectFromList(int i)
    {
        RandomPitch();
        _sfxAdAudioSource.PlayOneShot(AudioClips[i]);
    }

    public void PlayEffectFromExternal(AudioClip c)
    {
        RandomPitch();
        _sfxAdAudioSource.PlayOneShot(c);
    }
}
