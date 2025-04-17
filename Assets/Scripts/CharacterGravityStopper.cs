using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravityStopper : MonoBehaviour
{
    CharacterManager player;
    void Start()
    {
        player = GameObject.Find("player").GetComponent<CharacterManager>();
    }

    public void CharacterStopper()
    {
        player.NormalGravity = false;
        player.rb.velocity = Vector2.zero;
        player.canWalk = false;
        player.canJump = false;
        player.rb.gravityScale = 0;
    }

    public void CharacterContinue()
    {
        player.NormalGravity = true;
        player.canWalk = true;
    }
  
}
