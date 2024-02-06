using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotationV2 : MonoBehaviour
{
    public Animator animator;
    public CharacterManager character;
    public bool collideDedection;
    public camRotationV2 instance;
    [SerializeField] bool isReply;
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
    

    private void Update()
    {
        TriggerSystem();
    }
    void TriggerSystem()
    {
        if (instance.collideDedection == true )
        {
            character.camrotate = true;
            character.right90 = true;
            instance.animator.SetBool("triggeractivate", true);
        }
        else if(instance.collideDedection == false && instance.isReply)
        {
            instance.animator.SetBool("triggeractivate", false);
        }
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
    /*
    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    */

}
