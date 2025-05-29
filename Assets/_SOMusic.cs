using UnityEngine;
[CreateAssetMenu(fileName = "Music", menuName = "Music")]
public class _SOMusic : ScriptableObject
{
    public AudioClip audioClip;
    public string Music_Name;
    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;
    [Range(0f, 1.0f)]
    public float Volume;
}
