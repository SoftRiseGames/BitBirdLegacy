using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TrambolineVoiceManager : MonoBehaviour
{
    public static TrambolineVoiceManager instance;
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
        if (audioSource.isPlaying)
            audioSource.clip = null;
        audioSource.clip = Sfx[i].audioClip;
        audioSource.pitch = Sfx[i].pitch;
        audioSource.volume = Sfx[i].Volume;
        audioSource.Play();
        /*
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
        */

    }
}
