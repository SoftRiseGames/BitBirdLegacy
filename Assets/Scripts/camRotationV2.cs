using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotationV2 : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;


    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("player").GetComponent<CharacterManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
           
            character.camrotate = true;
           
        }
    }
    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        

        if (character.camrotate == true )
        {
           
            animator.SetBool("triggeractivate", true);
            StartCoroutine(animtimer());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            character.camrotate = false;
        }
    }

    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetBool("triggeractivate", false);
    }

}
