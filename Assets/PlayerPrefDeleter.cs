using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefDeleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
