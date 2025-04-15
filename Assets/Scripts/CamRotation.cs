using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CamRotation : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;
    [SerializeField] bool isDoubleSide;
    public bool isleft;
    public bool isRight;
    public PlayerInput playerInput;
   
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
            if (collideDedection == true && playerInput.actions["Interactivity"].inProgress && !isRight && !isleft)
            {
                Debug.Log("eeee");
                character.camrotate = true;
                character.right90 = true;
                isRight = true;
                isleft = false;
                animator.SetBool("sagtrigger", true);

                //animator.SetBool("triggeractivate", true);
                //StartCoroutine(animtimer());
            }
            /*
            else if (collideDedection == true && Input.GetButton("negativeinteractivity") && !isRight && !isleft)
            {
                character.camrotate = true;
                character.left90 = true;
                isRight = false;
                isleft = true;
                animator.SetBool("soltrigger", true);
                //animator.SetBool("triggeractivate", true);
                //StartCoroutine(animtimer());
            }
            else if (collideDedection == true && Input.GetButton("negativeinteractivity") && isRight)
            {
                character.camrotate = true;
                character.left90 = true;
                isRight = false;
                isleft = false;
                animator.SetBool("sagdansolatrigger", true);
                //animator.SetBool("triggeractivate", true);
                //StartCoroutine(animtimer());
            }
            */
            else if (collideDedection == true && playerInput.actions["Interactivity"].IsPressed() && isleft)
            {
                Debug.Log("eeee");
                character.camrotate = true;
                character.right90 = true;
                isRight = false;
                isleft = false;
                animator.SetBool("soldansagatrigger", true);
                //animator.SetBool("triggeractivate", true);
                //StartCoroutine(animtimer());
            }
        }
        else if (!isDoubleSide && isRight == false)
        {
            if (collideDedection == true && playerInput.actions["Interactivity"].IsPressed())
            {
                Debug.Log("eeee");
                character.camrotate = true;
                character.right90 = true;
                isRight = true;
                animator.SetBool("sagtrigger", true);
                //animator.SetBool("triggeractivate", true);
                //StartCoroutine(animtimer());
            }
        }
        
    }
    public void animatorReset()
    {
        
        animator.SetBool("sagtrigger", false);
        animator.SetBool("sagdansolatrigger", false);
        animator.SetBool("soldansagatrigger", false);
        animator.SetBool("soltrigger", false);
        
    }

    
    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("triggeractivate", false);
    }
    
  
}
