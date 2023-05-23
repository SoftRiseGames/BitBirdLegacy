using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystemCodes : MonoBehaviour
{
    public CharacterManager player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (player.canWalk)
            player.Walk(player.movementVeriable);


        if (Input.GetKeyDown(KeyCode.Space) && player.canJump && player.canDash && !player.DashTimerControl)
        {
            player.Jump();
            player.jumpEffect();
        }


        if (Input.GetKeyDown(KeyCode.Space) && player.secondJump && !player.canJump && player.canDash && player.doubleJumpControl)
            player.DoubleJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash && player.dashControl)
        {
            StartCoroutine(player.Dash(player.dashTimer));
            player.DashEffect();
        }
      
    }
}
