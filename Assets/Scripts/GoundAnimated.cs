using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundAnimated : MonoBehaviour
{
    public Animator animator;
    bool isRevive;
    bool isStartedBreak;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        CharacterManager.isGround += isReviveCheck;
    }
    private void OnDisable()
    {
        CharacterManager.isGround -= isReviveCheck;
    }

    private void Update()
    {
       
        Debug.Log(isStartedBreak);
    }
    void isReviveCheck() 
    {
        if (isStartedBreak)
            isRevive = true;
        else
            isRevive = false;

        GroundRevive();
    }

    public void isBreakStartCheck() 
    {
        isStartedBreak = true;
    } 

    public void AnimatorStarted()
    {
        animator.SetBool("animatedground", true);
    }

    public void isBreakStopCheck() => isStartedBreak = false;


    void GroundRevive()
    {
        if (isRevive)
        {
            animator.SetBool("isReactivate", true);
        }
            
    }

    
    public void GroundZeroAnimation()
    {
        animator.SetBool("isReactivate", false);
        animator.SetBool("animatedground", false);
    }
}
