using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class PauseModeManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuFirstSelectedButton;
    [SerializeField] GameObject OptionsMenuFirstObject;
    [SerializeField] GameObject KeyMappingFirstSelect;
    [SerializeField] List<GameObject> Canvases;
    [SerializeField] GameObject PauseScene;
    public PlayerInput playerInput;
    private void Awake()
    {
        PauseScene.SetActive(false);
    }
    void Update()
    {
        if (playerInput.actions["MainMenu"].IsPressed())
        {
            PauseScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OptionsButtonEvent()
    {
        Canvases[0].gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsMenuFirstObject);
    }
    public void KeyMappingButtonEvent()
    {
        Canvases[1].gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(KeyMappingFirstSelect);
    }
    public void QuitToMain()
    {
        foreach (GameObject i in Canvases)
            i.gameObject.SetActive(false);
    
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseMenuFirstSelectedButton);
    }
    public void BackTheGame()
    {
        PauseScene.gameObject.SetActive(false);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
    public void BackToConsoleScreen()
    {
        
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
   

}
