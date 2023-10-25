using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotationV2 : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;
    public camRotationV2 instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        animator =GetComponent<Animator>();
        character = GameObject.Find("player").GetComponent<CharacterManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            instance.collideDedection = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            instance.collideDedection = false;
            character.camrotate = false;
        }
    }
    

    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        if (collideDedection == true )
        {
            character.camrotate = true;
            instance.animator.SetBool("triggeractivate", true);
            //StartCoroutine(animtimer());
        }
    }
 
    /*
    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetBool("triggeractivate", false);
    }*/

}
