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
    [SerializeField] int saverinteger;
   // [SerializeField] GameObject emptykapi;
    [SerializeField] GameObject fullkapi;
    bool iscollide;
    public kapi instance;
    private void Awake()
    {
        if (character == null)
        {
            character = GameObject.Find("player").GetComponent<CharacterManager>();
        }
        if (instance == null)
        {
            instance = this;
        }
        if (PlayerPrefs.HasKey(this.gameObject.name))
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
        if (instance.saverinteger == 0)
        {
            instance.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        }
        else if (instance.saverinteger == 1)
        {
            instance.gameObject.transform.position = new Vector2(dotweengidisPoint.transform.position.x, dotweengidisPoint.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (iscollide && saverinteger == 0)
        {
          
            if (character.isFollow && Input.GetKey(KeyCode.E))
            {
                character.isFollow = false;
                character.transform.GetChild(2).GetComponent<KeyScript>().Suicide();
                instance.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear);
                instance.saverinteger = 1;
                PlayerPrefs.SetInt(this.gameObject.name, instance.saverinteger);
                
            }
        }
        if (Vector2.Distance(character.transform.position, gameObject.transform.position) < 3)
        {
            instance.fullkapi.SetActive(true);
            /*
            if (character.isFollow)
            {
                instance.emptykapi.SetActive(false);
                instance.fullkapi.SetActive(true);
            }
            else if (!character.isFollow)
            {
                instance.emptykapi.SetActive(true);
                instance.fullkapi.SetActive(false);
            }
            */
        }
        else
        {
            //instance.emptykapi.SetActive(false);
            instance.fullkapi.SetActive(false);
        }

    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            instance.iscollide = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            instance.iscollide = false;
        }
    }

}
