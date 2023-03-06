using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObjects : MonoBehaviour
{
    public GravityObjects thisObject;
    void Start()
    {
        if(thisObject == null)
        {
            thisObject = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterTurn(float gravityX, float gravityY)
    {
        Physics2D.gravity = new Vector2(gravityX, gravityY);
    }
}
