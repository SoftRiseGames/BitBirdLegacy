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

    public int ÝsRotateObjectAgain { get; set; }

    //int isReply;

    private void Awake()
    {
        ÝsRotateObjectAgain = 1;
    }

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
        RotateObject();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && ÝsRotateObjectAgain == 1)
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

    public void RotateObject()
    {
        if (collideDedection == true && isRight && ÝsRotateObjectAgain == 1)
        {
            character.camrotate = true;
            ÝsRotateObjectAgain = 0;
            character.right90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if (collideDedection == true && isLeft && ÝsRotateObjectAgain == 1)
        {
            character.camrotate = true;
            ÝsRotateObjectAgain = 0;
            character.left90 = true;
            animator.SetBool("triggeractivate", true);
        }
        else if (collideDedection == false && ÝsRotateObjectAgain == 0)
        {
            animator.SetBool("triggeractivate", false);
        }
    }

    public void IfStartOff()
    {
        Debug.Log("deactivate");
        ÝsRotateObjectAgain = 0;
        animator.SetBool("triggeractivate", true);
    }
}
