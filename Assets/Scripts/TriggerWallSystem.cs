using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWallSystem : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (Input.GetButton("interactivity"))
            {
                animator.SetBool("isBreak",true);
                floor.GetComponent<Animator>().SetBool("isBreak", true);
            }
        }
       
    }


}
