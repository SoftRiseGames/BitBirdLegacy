using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
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
        if (collision.gameObject.tag == "player")
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
        if(character.camrotate ==true && Input.GetKey(KeyCode.X))
        {
            Debug.Log("a");
            animator.SetBool("triggeractivate", true);
            StartCoroutine(animtimer());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            character.camrotate = false;
        }
    }

    IEnumerator animtimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("triggeractivate", false);
    }
  
}
