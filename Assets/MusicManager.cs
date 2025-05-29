using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    public List<_SOMusic> MusicFiles;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MusicFiles[0].audioClip;
    }

    
    void Update()
    {
        MusicControl();
    }
    void MusicControl()
    {
        audioSource.volume = MusicFiles[0].Volume * (PlayerPrefs.GetFloat("MusicVolume") / 10);
    }
}
