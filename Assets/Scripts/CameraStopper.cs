using UnityEngine;

public class CameraStopper : MonoBehaviour
{
    [SerializeField] CharacterManager player;
    
    public void StopCharacter()
    {
        player.canWalk = false;
        player.canJump = false;
        player.NormalGravity = false;
        player.rb.velocity = Vector2.zero;
       
    }

    public void ContinueCharacter()
    {
        player.NormalGravity = true;
        player.canWalk = true;
    }
}
