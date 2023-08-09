using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ConsoleSelectedSystem : MonoBehaviour, ISelectHandler,IPointerClickHandler// required interface when using the OnSelect method.
{
    
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Fare ile t�klama oldu�unda butonun se�ili durumunu engelle
        EventSystem.current.SetSelectedGameObject(null);
    }
    



}