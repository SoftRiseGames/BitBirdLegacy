using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using System;
public class kapi : MonoBehaviour
{
    [SerializeField] GameObject dotweengidisPoint;
    [SerializeField] float gidis;
    [SerializeField] float gelis;
    [SerializeField] CharacterManager character;
    public int saverinteger;
    [SerializeField] GameObject fullkapi;
    public bool isPass;
    public bool iscollide;
    Vector2 objectStartTransform;

    public PlayerInput PlayerInput;
    public static Action isNonMove;
    public static Action isMove;
    private void Awake()
    {
        objectStartTransform = this.gameObject.transform.position;
        if (character == null)
        {
            character = GameObject.Find("player").GetComponent<CharacterManager>();
        }
        

        if (PlayerPrefs.HasKey(gameObject.transform.name))
        {
            saverinteger = 1;
        }
        else
        {
            saverinteger = 0;
        }

       
    }
    void Update()
    {
       
        if (iscollide && saverinteger == 0)
        {
          
            if (character.isFollow && PlayerInput.actions["Interactivity"].IsPressed())
            {
               
                isNonMove.Invoke();
                DoorVoiceManager.instance.SFXSoundPlay(1);
                character.isFollow = false;
                character.transform.GetChild(6).GetComponent<KeyScript>().Suicide();
                gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear).OnComplete(() => isMove.Invoke());
            }
        }
        if (Vector2.Distance(character.transform.position, gameObject.transform.position) < 3 &&saverinteger == 0)
        {
            fullkapi.SetActive(true);
        }
        else
        {
            fullkapi.SetActive(false);
        }
    }
    public void isPassControl()
    {
        isNonMove.Invoke();
        DoorVoiceManager.instance.SFXSoundPlay(1);
        gameObject.transform.DOMove(objectStartTransform, gidis).SetEase(Ease.Linear).OnComplete(() => { isMove.Invoke(); });
        saverinteger = 1;
        PlayerPrefs.SetInt(gameObject.transform.name, saverinteger);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" &&saverinteger==0)
        {
            iscollide = true;
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" && saverinteger == 0)
        {
            iscollide = false;
        }
    }
}
