using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    Vector3 BaseScale;
    void Start()
    {
        BaseScale = transform.localScale;
       
    }

    private void Update()
    {
        ButtonControl();
    }

    void ButtonControl()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            transform.DOScale(1.35f, .1f);
        }
        else
        {
            transform.DOScale(BaseScale, .1f);
        }
    }
    
}
