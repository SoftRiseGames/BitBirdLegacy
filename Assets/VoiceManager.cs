using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class VoiceManager : MonoBehaviour
{
    public static VoiceManager instance;
    public AudioSource audioSource;

    [TabGroup("SFX Sounds")]
    public List<_SOSfx> Sfx;
    [TabGroup("Music Sounds")]
    public List<AudioClip> Voice;
    [TabGroup("Music Settings")]
    public float MusicVolumeSettings;
    [TabGroup("SFX Settings")]
    public float SFXVolumeSettings;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
            instance = this;
    }

    public void SFXSoundPlay(int i)
    {
        audioSource.clip = null;
        audioSource.clip = Sfx[i].audioClip;
        audioSource.pitch = Sfx[i].pitch;
        audioSource.volume = Sfx[i].Volume*(PlayerPrefs.GetFloat("SFXVolume")/10);
        audioSource.Play();
        
    }
}
