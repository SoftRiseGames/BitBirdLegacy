using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MainMenuOpener : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField] GameObject MainMenu;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (playerInput.actions["MainMenu"].IsPressed())
        {
            Time.timeScale = 0;
            MainMenu.SetActive(true);
        }
    }


}
