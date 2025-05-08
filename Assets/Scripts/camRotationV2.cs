using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotationV2 : MonoBehaviour,IRotate
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;


    [SerializeField] bool isLeft;
    [SerializeField] bool isRight;

    public int �sRotateObjectAgain { get; set; }

    //int isReply;



    void Start()
    {
        animator =GetComponent<Animator>();
        character = GameObject.Find("player").GetComponent<CharacterManager>();

        if (PlayerPrefs.HasKey(gameObject.name + "_canReply"))
            �sRotateObjectAgain = 0;
        else
            �sRotateObjectAgain = 1;
    }
    

    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        RotateObject();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && �sRotateObjectAgain == 1)
        {
            collideDedection = true;
            �sRotateObjectAgain = 0;
          
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

    public void RotateObject()
    {
        if (collideDedection == true && isRight)
        {
            character.camrotate = true;
            character.right90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if (collideDedection == true && isLeft)
        {
            character.camrotate = true;
            character.left90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if (collideDedection == false && �sRotateObjectAgain == 0)
        {
            animator.SetBool("triggeractivate", false);
        }
    }

    public void IfStartOff()
    {
        �sRotateObjectAgain = 0;
        animator.SetBool("triggeractivate", true);
    }
}
