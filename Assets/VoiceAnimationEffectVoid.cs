using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceAnimationEffectVoid : MonoBehaviour
{
    public void DashPlay()
    {
        VoiceManager.instance.SFXSoundPlay("Dash");
    }
    public void WalkPlay()
    {
        VoiceManager.instance.SFXSoundPlay("Walk");
    }
    public void DeathPlay()
    {
        VoiceManager.instance.SFXSoundPlay("Death");
    }
    public void JumpPlay()
    {
        VoiceManager.instance.SFXSoundPlay("Jump");
    }
    public void TrambolinePlay()
    {
        VoiceManager.instance.SFXSoundPlay("Tramboline");
    }
}
