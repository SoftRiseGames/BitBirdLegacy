using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using Sirenix.OdinInspector;
public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource cameraShakeManager;

    public static CameraShake instance;

    [TabGroup("Tramboline")]
    [SerializeField] float TrambolineAmplitudeGain;
    [TabGroup("Tramboline")]
    [SerializeField] float TrambolineFrequencyGain;
    [TabGroup("Tramboline")]
    [SerializeField] float TrambolineCamShakeTimer;
    [TabGroup("Dash")]
    [SerializeField] float DashAmplitudeGain;
    [TabGroup("Dash")]
    [SerializeField] float DashFrequencyGain;
    [TabGroup("Dash")]
    [SerializeField] float DashCamShakeTimer;

    void Start()
    {
        if (instance == null)
            instance = this;


        cameraShakeManager = GetComponent<CinemachineImpulseSource>();
    }
    public void DashShake()
    {
        Debug.Log("CameraShake");
        cameraShakeManager.m_ImpulseDefinition.m_AmplitudeGain = DashAmplitudeGain;
        cameraShakeManager.m_ImpulseDefinition.m_FrequencyGain = DashFrequencyGain;
        cameraShakeManager.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = DashCamShakeTimer;

        cameraShakeManager.GenerateImpulse();
    }

    public void TrambolineShake()
    {
        cameraShakeManager.m_ImpulseDefinition.m_AmplitudeGain = TrambolineAmplitudeGain;
        cameraShakeManager.m_ImpulseDefinition.m_FrequencyGain = TrambolineFrequencyGain;
        cameraShakeManager.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = TrambolineCamShakeTimer;

        cameraShakeManager.GenerateImpulse();
    }


  
}
