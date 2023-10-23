using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GameObject.Find("player").GetComponent<CharacterManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collideDedection = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collideDedection = false;
            character.camrotate = false;
        }
    }
    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        if(collideDedection ==  true && Input.GetKey(KeyCode.X))
        {
            character.camrotate = true;
            animator.SetBool("triggeractivate", true);
            StartCoroutine(animtimer());
        }
    }
 

    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("triggeractivate", false);
    }
  
}
