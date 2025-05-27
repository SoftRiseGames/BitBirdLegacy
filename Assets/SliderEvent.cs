using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderEvent : MonoBehaviour
{
    float MusicVolume;
    float SFXVolume;
    [SerializeField] bool isMusicSlider;
    [SerializeField] bool isSFXSlider;
    [SerializeField] List<GameObject> SliderValue;
    [SerializeField] Image BarBG;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume") && isMusicSlider == true)
        {
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            gameObject.GetComponent<Slider>().value = MusicVolume;
        }
        else if(isMusicSlider == true)
        {
            MusicVolume = 100;
            gameObject.GetComponent<Slider>().value = MusicVolume;
        }

        if (PlayerPrefs.HasKey("SFXVolume") && isSFXSlider == true)
        {
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            gameObject.GetComponent<Slider>().value = SFXVolume;
        }
        else if (isSFXSlider == true)
        {
            SFXVolume = 100;
            gameObject.GetComponent<Slider>().value = SFXVolume;
        }
        else
            return;

    }
    private void Update()
    {
        VoiceManager.instance.MusicVolumeSettings = MusicVolume;
        VoiceManager.instance.SFXVolumeSettings = SFXVolume;

        valueChange();
        ColorChange();
    }


    public void MusicValueChange()
    {
        MusicVolume = gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }
    public void SFXValueChange()
    {
        SFXVolume = gameObject.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
    }

    void ColorChange()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            BarBG.color = Color.red;
        }
        else
        {
            BarBG.color = Color.white;
        }
    }
    void valueChange()
    {
        for(int i = 0; i<=9; i++)
        {
            if(i< gameObject.GetComponent<Slider>().value)
            {
                SliderValue[i].SetActive(true);
            }
            else
            {
                SliderValue[i].SetActive(false);
            }
        }
    }
}
