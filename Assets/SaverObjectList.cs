using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaverObjectList : MonoBehaviour
{
    public List<GameObject> RotationObjects;
    public List<GameObject> KeyList;
    public int isTouched = 0;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(gameObject.name + "_isSave"))
            isTouched = 1;
        else
            isTouched = 0;
    }
    private void Start()
    {
        if (isTouched == 1)
        {
           
            foreach (GameObject i in RotationObjects)
            {
                i.GetComponent<IRotate>().IfStartOff();
            }
            if(KeyList != null)
            {
                foreach (GameObject i in KeyList)
                {
                    i.SetActive(false);
                    
                }
            }
           
        }
        else
            return;
     
    }
    public void isTouchVoid()
    {
        isTouched = 1;
        Debug.Log("Saved");
        PlayerPrefs.SetInt(gameObject.name + "_isSave", isTouched);
    }
   
}
