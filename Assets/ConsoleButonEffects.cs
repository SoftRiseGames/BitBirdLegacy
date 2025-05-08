using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ConsoleButonEffects : MonoBehaviour
{
    [SerializeField] Image BirdImage;
    void Start()
    {
        
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            BirdImage.gameObject.SetActive(true);
        }
        else
        {
            BirdImage.gameObject.SetActive(false);
        }
    }
}
