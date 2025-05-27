using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefDeleter : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
    }

}
