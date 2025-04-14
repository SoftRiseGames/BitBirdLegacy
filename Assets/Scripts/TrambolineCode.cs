using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrambolineCode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(collision.GetComponent<CharacterManager>().TrambolineAddForce(transform,gameObject.transform.eulerAngles.z));
            Debug.Log(gameObject.transform.eulerAngles.z);
        }
    }
   
}
    