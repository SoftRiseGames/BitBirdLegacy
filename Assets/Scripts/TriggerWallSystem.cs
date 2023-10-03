using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWallSystem : MonoBehaviour
{
    [SerializeField] GameObject floor;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(floor);
            }
        }
       
    }


}
