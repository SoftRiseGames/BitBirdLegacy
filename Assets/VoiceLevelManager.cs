using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLevelManager : MonoBehaviour
{
    public float SFXValue;
    public float MusicValue;
    public VoiceLevelManager instance;
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
