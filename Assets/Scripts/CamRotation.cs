using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;
    [SerializeField] bool isDoubleSide;
    bool isleft;
    bool isRight;
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
        if (isDoubleSide)
        {
            if (collideDedection == true && Input.GetButton("interactivity") && !isRight && !isleft)
            {
                character.camrotate = true;
                character.right90 = true;
                isRight = true;
                isleft = false;
                animator.SetBool("triggeractivate", true);
                StartCoroutine(animtimer());
            }
            else if (collideDedection == true && Input.GetButton("negativeinteractivity") && !isRight && !isleft)
            {
                character.camrotate = true;
                character.left90 = true;
                isRight = false;
                isleft = true;
                animator.SetBool("triggeractivate", true);
                StartCoroutine(animtimer());
            }
            else if (collideDedection == true && Input.GetButton("negativeinteractivity") && isRight)
            {
                character.camrotate = true;
                character.left180 = true;
                isRight = false;
                isleft = true;
                animator.SetBool("triggeractivate", true);
                StartCoroutine(animtimer());
            }
            else if (collideDedection == true && Input.GetButton("interactivity") && isleft)
            {
                character.camrotate = true;
                character.right180 = true;
                isRight = true;
                isleft = false;
                animator.SetBool("triggeractivate", true);
                StartCoroutine(animtimer());
            }
        }
        else if (!isDoubleSide)
        {
            if (collideDedection == true && Input.GetButton("interactivity") )
            {
                character.camrotate = true;
                character.right90 = true;
                animator.SetBool("triggeractivate", true);
                StartCoroutine(animtimer());
            }
        }
        
    }
 
    
    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("triggeractivate", false);
    }
    
  
}
