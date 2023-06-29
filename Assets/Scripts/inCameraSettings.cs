using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class inCameraSettings : MonoBehaviour
{
    public inCameraSettings thisCamera;
    public Animator animator;
    
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
        character = GameObject.Find("player").GetComponent<CharacterManager>();
        camShake = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
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
                character.beginningGravityX = 9.8f;
                character.beginningGravityy = 0;
               
            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 90)
            {
                character.beginningGravityX = 0;
                character.beginningGravityy = 9.8f;
               
            }
            if (this.gameObject.transform.rotation.z == 1)
            {
                character.beginningGravityX = -9.8f;
                character.beginningGravityy = 0;
               
            }
            if (this.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 270)
            {
                character.beginningGravityX = 0;
                character.beginningGravityy = -9.8f;
              
            }
            cevir(character.beginningGravityX, character.beginningGravityy);
           

        }
    }

    public void shake(float amplitude, float dur)
    {
        SetShake(amplitude, dur * .15f).OnComplete(() => SetShake(0, dur * .15f).SetDelay(dur * .7f));
    }

    public DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> SetShake(float amplitude, float dur)
    {
        return DOTween.To(
             () => camShake.m_AmplitudeGain,
             val => camShake.m_AmplitudeGain = val,
             amplitude,
             dur
         );
    }
    void cevir(float acix, float aciy)
    {
        character.rb.velocity = new Vector3(0, 0, 0);
        character.CharacterTurn(acix, aciy);
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


        if (thisCamera.gameObject.transform.rotation.z == 0)
        {

            thisCamera.animator.SetBool("90", true);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", false);

        }
        else if (thisCamera.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 180)
        {

            thisCamera.animator.SetBool("180", true);
            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", false);
        }

        if (thisCamera.gameObject.transform.rotation.z == 1)
        {

            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", true);
            thisCamera.animator.SetBool("360", false);
        }

        if (thisCamera.gameObject.transform.rotation.z == 0.7071068f && character.rotationz == 0)
        {

            thisCamera.animator.SetBool("90", false);
            thisCamera.animator.SetBool("180", false);
            thisCamera.animator.SetBool("270", false);
            thisCamera.animator.SetBool("360", true);
        }
        PlayerPrefs.SetFloat("rotationz", character.rotationz);
      

    }

   
}