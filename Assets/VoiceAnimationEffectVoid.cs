using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceAnimationEffectVoid : MonoBehaviour
{

    public void DashPlay()
    {
        VoiceManager.instance.SFXSoundPlay(1);
    }
    public void WalkPlay()
    {
        //VoiceManager.instance.SFXSoundPlay(3);
    }
    public void DeathPlay()
    {
        VoiceManager.instance.SFXSoundPlay(0);
    }
    public void JumpPlay()
    {
        VoiceManager.instance.SFXSoundPlay(2);
    }
    public void TrambolinePlay()
    {
        TrambolineVoiceManager.instance.SFXSoundPlay(0);
    }
    public void RotationPlay()
    {
        
    }
}
