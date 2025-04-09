using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public bool draw;
    public bool eraser;
    public GameObject tryInstantiate;
    public int gameobjectIncrease;
    void Start()
    {
       
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
    public void instantiator()
    {
        Instantiate(tryInstantiate);
    }
    public void increaseValue()
    {
        gameobjectIncrease = gameobjectIncrease + 1;
    }
    public void decreaseValue()
    {
        gameobjectIncrease = gameobjectIncrease - 1;
    }
}
