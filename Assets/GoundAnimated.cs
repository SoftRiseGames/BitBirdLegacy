using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundAnimated : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            animator.SetBool("animatedground", true);
            StartCoroutine(groundTimer());
        }
    }
    IEnumerator groundTimer()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("animatedground", false);
    }

}
