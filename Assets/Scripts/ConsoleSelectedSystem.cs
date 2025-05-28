using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class ConsoleSelectedSystem : MonoBehaviour
{
    private CinemachineVirtualCamera StandardCamera;
    public List<CinemachineVirtualCamera> ConnectedCamera;
    public List<GameObject> ConsoleAfterSelectionFirstButtonSelect;
    [SerializeField] List<Button> ConsoleButtons;
    private CinemachineVirtualCamera selectedSpecialCamera;
    [SerializeField] GameObject FirstSelectedButton;
    [SerializeField] List<Canvas> AllCanvas;
    [SerializeField] GameObject PauseMenu;
    public List<GameObject> OptionsButtons;
    public List<GameObject> OptionsSelectionButtons;
    [SerializeField] GameObject MainCamera;
    float defaultblend;
    [TabGroup("OptionFirstSelectionButton")]
    [SerializeField] GameObject PauseMenuFirstObject;
    [TabGroup("OptionFirstSelectionButton")]
    [SerializeField] GameObject KeyMappingFirstObject;
    [TabGroup("OptionFirstSelectionButton")]
    [SerializeField] GameObject OptionsMenuFirstObject;

    private void Awake()
    {
        StandardCamera = GameObject.Find("StandardCamera").GetComponent<CinemachineVirtualCamera>();
        defaultblend = MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time;
    }
 
    
    void ConsoleOff()
    {
        foreach(Button i in ConsoleButtons)
        {
            i.interactable = false;
        }
        foreach (Canvas i in AllCanvas)
        {
            i.sortingOrder = 11 ;
        }

    }
    void ConsoleOn()
    {
        foreach(Button i in ConsoleButtons)
        {
            i.interactable = true;
        }
        foreach (Canvas i in AllCanvas)
        {
            i.sortingOrder = 0;
        }
    }
    public void DoActionForFirstConsoleSelect()
    {
        Debug.Log("firstconsole");
        selectedSpecialCamera = ConnectedCamera[0];
        ConsoleOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ConsoleAfterSelectionFirstButtonSelect[0]);
        StandardCamera.gameObject.SetActive(false);
        selectedSpecialCamera.gameObject.SetActive(true);
    }
    public void DoActionForSecondConsoleSelect()
    {
        selectedSpecialCamera = ConnectedCamera[1];
        ConsoleOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ConsoleAfterSelectionFirstButtonSelect[1]);
        StandardCamera.gameObject.SetActive(false);
        selectedSpecialCamera.gameObject.SetActive(true);
    }
    public void DoActionForThirdConsoleSelect()
    {
        selectedSpecialCamera = ConnectedCamera[2];
        ConsoleOff();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ConsoleAfterSelectionFirstButtonSelect[2]);
        StandardCamera.gameObject.SetActive(false);
        selectedSpecialCamera.gameObject.SetActive(true);
    }

    public void QuitToMain()
    {
        EventSystem.current.SetSelectedGameObject(null);
        ConsoleOn();
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);
        selectedSpecialCamera.gameObject.SetActive(false);
        StandardCamera.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OptionsMenuOpen()
    {
        OptionsButtons[0].gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsMenuFirstObject);
    }
    public void KeyMappingMenuOpen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(KeyMappingFirstObject);
        OptionsButtons[1].gameObject.SetActive(true);
    }
    public void GoToPauseScreen()
    {
        foreach(CinemachineVirtualCamera i in ConnectedCamera)
        {
            i.gameObject.SetActive(false);
        }
        MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0;
        StandardCamera.gameObject.SetActive(true);
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseMenuFirstObject);
    }
    public void GoToConsoleSelectionScreen()
    {
       
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);
        ConsoleOn();
        MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = defaultblend;
        PauseMenu.SetActive(false);
    }
    public void GoToOptionsMenu()
    {
        foreach(GameObject i in OptionsButtons)
        {
            i.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseMenuFirstObject);
    }
}