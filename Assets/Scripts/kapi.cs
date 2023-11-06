using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    //kapalý kapý sistemi
    Vector2 objectStartTransform;
    public kapi instance;
    private void Awake()
    {
        objectStartTransform = this.gameObject.transform.position;
        if (character == null)
        {
            character = GameObject.Find("player").GetComponent<CharacterManager>();
        }
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey(gameObject.transform.name))
        {
            instance.saverinteger = 1;
        }
        else
        {
            instance.saverinteger = 0;
        }

       
    }
    void Start()
    {
        /*
        if (instance.saverinteger == 0)
        {
            instance.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        }
        else if (instance.saverinteger == 1)
        {
            instance.gameObject.transform.position = new Vector2(dotweengidisPoint.transform.position.x, dotweengidisPoint.transform.position.y);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
       
        if (iscollide && saverinteger == 0)
        {
          
            if (character.isFollow && Input.GetButton("interactivity"))
            {
                character.isFollow = false;
                character.transform.GetChild(2).GetComponent<KeyScript>().Suicide();
                instance.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear);
            }
        }
        if (Vector2.Distance(character.transform.position, gameObject.transform.position) < 3 &&instance.saverinteger == 0)
        {
            instance.fullkapi.SetActive(true);
        }
        else
        {
            //instance.emptykapi.SetActive(false);
            instance.fullkapi.SetActive(false);
        }
    }
    public void isPassControl()
    {
        instance.gameObject.transform.DOMove(objectStartTransform, gidis).SetEase(Ease.Linear);
        instance.saverinteger = 1;
        PlayerPrefs.SetInt(gameObject.transform.name, instance.saverinteger);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" &&instance.saverinteger==0)
        {
            instance.iscollide = true;
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player" && instance.saverinteger == 0)
        {
            instance.iscollide = false;
        }
    }
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            instance.iscollide = false;
        }
    }
    */
}
