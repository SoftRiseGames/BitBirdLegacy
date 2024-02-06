using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cinemachine;

public class inCameraSettings : MonoBehaviour
{
    
    public inCameraSettings thisCamera;
    public Animator animator;
    public cameraMove CameraMove;
    public GameObject limitPoint;
    public CharacterManager character;
    public List<GameObject> objects = new List<GameObject>();
    public CinemachineBasicMultiChannelPerlin camShake;
    public float yukselishakeAmplitude;
    public float dususshakeAmplitude;
    public float yukselikduration;
    public float delay;
    public CharacterManager characterManager;
    public float startPrefs;
    

    private void Start()
    {
        if (characterManager == null)
        {
            characterManager = GameObject.Find("player").GetComponent<CharacterManager>();
        }
      
        if (thisCamera == null)
        {
            thisCamera = this;
        }

        if(CameraMove == null)
        {
            CameraMove = thisCamera.gameObject.transform.parent.GetComponent<cameraMove>();
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
        if (characterManager.camrotate)
        {
            if (this.gameObject.transform.rotation.z == 0)
            {
                CameraMove.istourEnd = false;
                character.beginningGravityX = 9.8f;
                character.beginningGravityy = 0;
               
            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 90)
            {
                CameraMove.istourEnd = true;
                character.beginningGravityX = 0;
                character.beginningGravityy = 9.8f;
               
            }
            if (this.gameObject.transform.rotation.z == 1)
            {
                CameraMove.istourEnd = true;
                character.beginningGravityX = -9.8f;
                character.beginningGravityy = 0;
               
            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 270)
            {
                CameraMove.istourEnd = false;
                character.beginningGravityX = 0;
                character.beginningGravityy = -9.8f;
              
            }
            cevir(character.beginningGravityX, character.beginningGravityy);
           

        }
    }

    void cevir(float acix, float aciy)
    {
        character.rb.velocity = new Vector3(0, 0, 0);
        character.rotationz = character.rotationz + 90;

      
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<GravityObjects>().CharacterTurn(acix, aciy);
        }
        if (character.rotationz > 270)
        {
            character.rotationz = 0;
        }
        
        character.transform.rotation = Quaternion.Euler(0, 0, character.rotationz);
        rotationTimer();
        PlayerPrefs.SetFloat("rotationz", character.rotationz);
      

    }
    public async void rotationTimer()
    {
        if (character.right90)
        {
            thisCamera.animator.SetBool("90", true);
            character.right90 = false;
            await Task.Delay(500);
            thisCamera.animator.SetBool("90", false);
        }
        else if (character.right180)
        {
            thisCamera.animator.SetBool("180", true);
            character.right180 = false;
            await Task.Delay(500);
            thisCamera.animator.SetBool("180", false);
        }
        else if (character.left90)
        {
            thisCamera.animator.SetBool("-90", true);
            character.left90 = false;
            await Task.Delay(500);
            thisCamera.animator.SetBool("-90", false);
        }
        else if (character.left180)
        {
            thisCamera.animator.SetBool("-180", true);
            character.left180 = false;
            await Task.Delay(500);
            thisCamera.animator.SetBool("-180", false);
        }
       
    }
  
   
}