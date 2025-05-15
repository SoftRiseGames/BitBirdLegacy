using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderEvent : MonoBehaviour
{
    public void MusicValueChange()
    {
        VoiceManager.instance.MUsicVolumeSettings = gameObject.GetComponent<Slider>().value;
    }
    public void SFXValueChange()
    {
        VoiceManager.instance.SFXVolumeSettings = gameObject.GetComponent<Slider>().value;
    }
}
