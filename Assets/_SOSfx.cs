using UnityEngine;
[CreateAssetMenu(fileName = "SFX", menuName ="SFX")]
public class _SOSfx : ScriptableObject
{
    public AudioClip audioClip;
    public string SFX_Name;
    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;
    [Range(0f, 1.0f)]
    public float Volume;
}
