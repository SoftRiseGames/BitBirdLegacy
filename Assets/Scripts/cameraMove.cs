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
    public string playerPrefsKey;
    public float savedRotation;
    public bool istourEnd;
    private void Start()
    {
        if(thiscollider == null)
        {
            thiscollider = this;
        }

        characterManagerCode = GameObject.Find("player").GetComponent<CharacterManager>();
        /*
        if (PlayerPrefs.HasKey("virtualrecord"))
        {
            thiscollider.startPrefs = PlayerPrefs.GetFloat("virtualrecord");
            virtualCam.gameObject.transform.rotation = Quaternion.Euler(virtualCam.gameObject.transform.rotation.x, virtualCam.gameObject.transform.rotation.y, thiscollider.startPrefs);
           
        }
        else
        {
            virtualCam.gameObject.transform.rotation = Quaternion.Euler(virtualCam.gameObject.transform.rotation.x, virtualCam.gameObject.transform.rotation.y, virtualCam.gameObject.transform.rotation.z);
        }
        */

        playerPrefsKey = "virtualrecord_" + virtualCam.name;
        
        // PlayerPrefs'te rotasyon değeri varsa, kameranın rotasyonu bu değerle ayarlanır
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            savedRotation = PlayerPrefs.GetFloat(playerPrefsKey);
            Debug.Log(savedRotation);
            if(savedRotation == 0)
            {
                virtualCam.transform.rotation = Quaternion.Euler(virtualCam.transform.rotation.x, virtualCam.transform.rotation.y, 0);
            }
            else if((savedRotation >= 0.7071060f  || savedRotation <= 0.70710670f) && !istourEnd )
            {
                virtualCam.transform.rotation = Quaternion.Euler(virtualCam.transform.rotation.x, virtualCam.transform.rotation.y, 270);
            }
            else if (savedRotation == 1)
            {
                virtualCam.transform.rotation = Quaternion.Euler(virtualCam.transform.rotation.x, virtualCam.transform.rotation.y, 180);
            }
            else if ((savedRotation >= 0.7071060f || savedRotation <= 0.70710670f) && istourEnd)
            {
                virtualCam.transform.rotation = Quaternion.Euler(virtualCam.transform.rotation.x, virtualCam.transform.rotation.y, 90);
            }

        }

    }
    private void Update()
    {
        /*
        if(virtualCam.gameObject.transform.rotation.z>=0 && virtualCam.gameObject.transform.rotation.z < 90)
        {
           istourEnd = false;
        }
        else
        {
            istourEnd = true;
        }
        */
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
            float currentRotation = virtualCam.transform.rotation.z;
            PlayerPrefs.SetFloat(playerPrefsKey, currentRotation);
            virtualCam.SetActive(false);
            Debug.Log(currentRotation);
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
