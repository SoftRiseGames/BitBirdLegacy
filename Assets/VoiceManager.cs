using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class VoiceManager : MonoBehaviour
{
    public static VoiceManager instance;
    private  AudioSource audioSource;

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

    public void SFXSoundPlay(string SfxName)
    {
        foreach(_SOSfx i in Sfx)
        {
            if (i.name == SfxName)
            {
                audioSource.clip = i.audioClip;
                audioSource.pitch = i.pitch;
                audioSource.volume = i.Volume;

                audioSource.Play();
            }
            
        }
       
    }
}
