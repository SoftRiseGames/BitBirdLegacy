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
    bool iscollide;
    public kapi instance;

    void Start()
    {
        if(character == null)
        {
            character = GameObject.Find("player").GetComponent<CharacterManager>();
        }
        if(instance == null)
        {
            instance = this;
        }


       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (iscollide)
        {
          
            if (character.isFollow && Input.GetKey(KeyCode.E))
            {
                character.isFollow = false;
                character.transform.GetChild(2).GetComponent<KeyScript>().Suicide();
                instance.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear);
            }
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
