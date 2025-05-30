using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerWallSystem : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] Animator animator;
    public PlayerInput playerInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (playerInput.actions["Interactivity"].IsPressed())
            {
                DoorVoiceManager.instance.SFXSoundPlay(2);
                animator.SetBool("isBreak",true);
                floor.GetComponent<Animator>().SetBool("isBreak", true);
            }
        }
       
    }


}
