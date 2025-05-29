using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefDeleter : MonoBehaviour
{
    float Volume = 10;
    float SFX =10;
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            Volume = PlayerPrefs.GetFloat("MusicVolume");
        else
            Volume = 10;

        if (PlayerPrefs.HasKey("SFXVolume"))
            SFX = PlayerPrefs.GetFloat("SFXVolume");
        else
            SFX = 10;

        Debug.Log(Volume);
        Debug.Log(SFX);
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("MusicVolume", Volume);
        PlayerPrefs.SetFloat("SFXVolume", SFX);
    }

}
