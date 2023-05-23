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


    void Start()
    {
        if(character == null)
        {
            character = GameObject.Find("player").GetComponent<CharacterManager>();
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
                character.transform.GetChild(0).GetComponent<KeyScript>().Suicide();
                this.gameObject.transform.DOMove(dotweengidisPoint.transform.position, gidis).SetEase(Ease.Linear);
            }
        }
        

    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        iscollide = true;
    }

}
