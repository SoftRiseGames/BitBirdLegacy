using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void SceneSkip()
    {
        Debug.Log("scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //demodan sonra silinecek
    private void Update()
    {
        /*
        if (Input.GetButton("reset"))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
        */
        /*
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            
        }
        */
    }

}
