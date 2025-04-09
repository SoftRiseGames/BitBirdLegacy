using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotationV2 : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;


    [SerializeField] bool isLeft;
    [SerializeField] bool isRight;
    [SerializeField] bool isReply;
   
    void Start()
    {
        animator =GetComponent<Animator>();
        character = GameObject.Find("player").GetComponent<CharacterManager>();
    }
    

    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        if (collideDedection == true && isRight)
        {
            character.camrotate = true;
            character.right90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if(collideDedection == true && isLeft)
        {
            character.camrotate = true;
            character.left90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if(collideDedection == false && isReply)
        {
            animator.SetBool("triggeractivate", false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
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
    /*
    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    */

}
