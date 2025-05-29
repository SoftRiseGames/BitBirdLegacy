using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    AudioSource audioSource;
    public List<_SOMusic> MusicFiles;

    public bool isGamePlay;
    void Awake()
    {
        if(isGamePlay == true)
        {
            if ((instance != null && instance != this) || (SceneManager.GetActiveScene().buildIndex == 0))
            {
                Destroy(gameObject); // Ayný tipte baþka MusicManager varsa kendini yok et
                return;

            }
            instance = this;
        }
     

        // Sadece GameObject adý GamePlay ve sahne ismi GamePlay ise DontDestroyOnLoad uygula
        if (gameObject.name == "GamePlay" && isGamePlay)
        {
            DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (MusicFiles.Count > 0)
        {
            audioSource.clip = MusicFiles[0].audioClip;
            audioSource.Play();
        }

        // Sahne deðiþikliklerini dinle
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Event dinlemesini kaldýr
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eðer sahne index'i 0 ise ve obje adý GamePlay ise kendini yok et
        if (scene.buildIndex == 0 && gameObject.name == "GamePlay")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        MusicControl();
    }

    void MusicControl()
    {
        if (MusicFiles.Count > 0)
        {
            audioSource.volume = MusicFiles[0].Volume * (PlayerPrefs.GetFloat("MusicVolume", 10f) / 10f);
        }
    }
}
