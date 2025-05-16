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
            BirdImage.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            BirdImage.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }
}
