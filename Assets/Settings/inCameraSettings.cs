using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class inCameraSettings : MonoBehaviour
{
    public inCameraSettings thisCamera;
    public Animator animator;
    public float rotationz;
    public GameObject limitPoint;
    public CharacterManager character;
    private void Start()
    {
       

        if (thisCamera == null)
        {
            thisCamera = this;
        }
        character = GameObject.Find("player").GetComponent<CharacterManager>();
       
    
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
            Debug.Log(thisCamera.gameObject.transform.rotation.z);
        GravityAndCamControl();
    }

    void GravityAndCamControl()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {

            if (this.gameObject.transform.rotation.z == 0)
            {
                
                cevir(9.8f, 0f);

            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && rotationz == 90)
            {
                cevir( 0f, 9.8f);
               
            }
            if (this.gameObject.transform.rotation.z == 1)
            {
                cevir(-9.8f, 0f);
         
          

            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && rotationz == 270)
            {
                cevir(0,-9.8f);
       
                
            }
        }
    }
    void cevir(float acix, float aciy)
    {
        Debug.Log("0");
        character.rb.velocity = new Vector3(0, 0, 0);
        character.CharacterTurn(acix, aciy);
        rotationz = rotationz + 90;
      
        if(rotationz> 270)
        {
            rotationz = 0;
        }

        character.transform.rotation = character.transform.rotation = Quaternion.Euler(0, 0, rotationz);
       
        if (thisCamera.gameObject.transform.rotation.z == 0)
        {
            Debug.Log("a");
            thisCamera.animator.SetBool("90", true);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", false);

        }
        else if (thisCamera.gameObject.transform.rotation.z == 0.7071068f && rotationz == 180)
        {
            Debug.Log("b");
            thisCamera.animator.SetBool("180", true);
            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", false);
        }

        if (thisCamera.gameObject.transform.rotation.z == 1)
        {
            Debug.Log("c");
            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", true);
            thisCamera.animator.SetBool("360", false);
        }

        if (thisCamera.gameObject.transform.rotation.z == 0.7071068f && rotationz == 0)
        {
            Debug.Log("d");
            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", true);
        }


    }
}
