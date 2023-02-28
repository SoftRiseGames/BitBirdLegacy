using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class inCameraSettings : MonoBehaviour
{
    public inCameraSettings thisCamera;
    public Animator animator;
    float rotationz = 0;
    public GameObject limitPoint;
    public CharacterManager character;
    private void Start()
    {
       

        if (thisCamera == null)
        {
            thisCamera = this;
        }
        character = GameObject.Find("player").GetComponent<CharacterManager>();
        //character.CharacterTurn(0, -9.8f);
        character.sagsolcont = false;
    }

    void Update()
    {
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
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.sagsolcont == false)
            {
                cevir( 0f, 9.8f);
               
            }
            if (this.gameObject.transform.rotation.z == 1)
            {
                cevir(-9.8f, 0f);
                character.sagsolcont = true;
          

            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.sagsolcont == true)
            {
                cevir(0,-9.8f);
                character.sagsolcont = false;
                
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
       
        if (this.gameObject.transform.rotation.z == 0)
        {
            animator.SetBool("90", true);
            animator.SetBool("180", false);
            animator.SetBool("270", false);
            animator.SetBool("360", false);

        }
        else if (this.gameObject.transform.rotation.z == 0.7071068f && character.sagsolcont == false)
        {
            animator.SetBool("180", true);
            animator.SetBool("90", false);
            animator.SetBool("270", false);
            animator.SetBool("360", false);
        }

        if (this.gameObject.transform.rotation.z == 1)
        {
            animator.SetBool("90", false);
            animator.SetBool("180", false);
            animator.SetBool("270", true);
            animator.SetBool("360", false);
        }

        if (this.gameObject.transform.rotation.z == 0.7071068f && character.sagsolcont == true)
        {
            animator.SetBool("90", false);
            animator.SetBool("180", false);
            animator.SetBool("270", false);
            animator.SetBool("360", true);
        }


    }
}
