using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class RotationVoiceManager : MonoBehaviour
{
    public static RotationVoiceManager instance;
    public AudioSource audioSource;

    [TabGroup("SFX Sounds")]
    public List<_SOSfx> Sfx;

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
        audioSource.volume = Sfx[i].Volume *(PlayerPrefs.GetFloat("SFXVolume")/10);
        audioSource.Play();
        
    }
}
