using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class cameraMove : MonoBehaviour
{
    public GameObject virtualCam;
    public CharacterManager characterManagerCode;

    private void Start()
    {
        characterManagerCode = GameObject.Find("player").GetComponent<CharacterManager>();
      
      
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            virtualCam.gameObject.SetActive(true);
            StartCoroutine(cameraWait());
        }
        
        if(collision.gameObject.tag == "gravitycontroller")
        {
            virtualCam.GetComponent<inCameraSettings>().objects.Add(collision.gameObject);
        }
       
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            virtualCam.gameObject.SetActive(false);
        }
    }

   

    IEnumerator cameraWait()
    {
        characterManagerCode.rb.velocity = Vector2.zero;
        characterManagerCode.canJump = false;
        characterManagerCode.canWalk = false;
        yield return new WaitForSeconds(.5f);
        characterManagerCode.canJump = true;
        characterManagerCode.canWalk = true;
    }

  

}
