using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCloseScript : MonoBehaviour
{
    public kapi kapi;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(kapi.saverinteger == 0)
        {
            kapi.isPassControl();
        }
        
    }
}
