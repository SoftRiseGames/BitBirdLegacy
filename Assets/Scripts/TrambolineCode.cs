using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrambolineCode : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(collision.GetComponent<CharacterManager>().TrambolineAddForce(transform,gameObject.transform.eulerAngles.z));
            animator.SetBool("isTramboline", true);
            Debug.Log(gameObject.transform.eulerAngles.z);
        }
    }

    public void TrambolineIdleSet()
    {
        animator.SetBool("isTramboline", false);

    }
   
}
    