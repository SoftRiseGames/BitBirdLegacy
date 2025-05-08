using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.InputSystem;

public class ConsoleSelectedSystem : MonoBehaviour
{
    private CinemachineVirtualCamera StandardCamera;
    public List<CinemachineVirtualCamera> ConnectedCamera;
    public List<GameObject> ConsoleAfterSelectionFirstButtonSelect;
    [SerializeField] List<Button> ConsoleButtons;
    private CinemachineVirtualCamera selectedSpecialCamera;
    [SerializeField] GameObject FirstSelectedButton;
    [SerializeField] List<Canvas> AllCanvas;
    private void Start()
    {
        StandardCamera = GameObject.Find("StandardCamera").GetComponent<CinemachineVirtualCamera>();
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
    



}