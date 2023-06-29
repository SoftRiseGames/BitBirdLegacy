using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class cameraMove : MonoBehaviour
{
    public GameObject virtualCam;
    public CharacterManager characterManagerCode;
    public float rotationStart;
    public float rotationControl;
    public float startPrefs;
    public cameraMove thiscollider;
    private void Start()
    {
        if(thiscollider == null)
        {
            thiscollider = this;
        }

        characterManagerCode = GameObject.Find("player").GetComponent<CharacterManager>();
        if (PlayerPrefs.HasKey("virtualrecord"))
        {
            this.startPrefs = PlayerPrefs.GetFloat("virtualrecord");
            virtualCam.gameObject.transform.rotation = Quaternion.Euler(virtualCam.gameObject.transform.rotation.x, virtualCam.gameObject.transform.rotation.y, 270);
           
        }
        else
        {
            virtualCam.gameObject.transform.rotation = Quaternion.Euler(virtualCam.gameObject.transform.rotation.x, virtualCam.gameObject.transform.rotation.y, virtualCam.gameObject.transform.rotation.z);
        }
        
      
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
            PlayerPrefs.SetFloat("virtualrecord", virtualCam.gameObject.transform.rotation.z);
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
