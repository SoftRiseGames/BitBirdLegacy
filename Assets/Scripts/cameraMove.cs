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
        if (thiscollider == null)
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

        virtualCam.transform.rotation = Quaternion.Euler(virtualCam.transform.rotation.x, virtualCam.transform.rotation.y, characterManagerCode.rotationz);

        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
              

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
        if (collision.gameObject.name == "player")
        {
            virtualCam.gameObject.SetActive(true);
        }

        if (collision.gameObject.tag == "gravitycontroller")
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




}