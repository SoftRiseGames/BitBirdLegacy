using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public bool draw;
    public bool eraser;
    
    void Start()
    {
        SoftriseLevelEditor.DrawListener += drawing;
        SoftriseLevelEditor.EraserListener += erase;
    }

    public void drawing()
    {
        draw = true;
        eraser = false;
        Debug.Log("draw");
    }
    public void erase()
    {
        draw = false;
        eraser = true;
        Debug.Log("erase");
    }
}
