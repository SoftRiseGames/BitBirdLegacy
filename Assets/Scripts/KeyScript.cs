using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyScript : MonoBehaviour
{
    public GameObject player;
    public KeyScript thisObject;
    public bool isTouch;
   
    void Start()
    {
        if(thisObject == null)
        {
            thisObject = this;
        }
        player = GameObject.Find("player");
    }

    
    void Update()
    {
        
        if (player.GetComponent<CharacterManager>().isFollow && isTouch == true)
        {
            if(player.gameObject.transform.rotation.z == 1 || player.gameObject.transform.rotation.z == -1 || player.gameObject.transform.rotation.z == 0)
            {
               
                this.gameObject.transform.parent = player.transform;
                if (player.gameObject.transform.localScale.x > 0)
                {
                    thisObject.transform.DOMove(new Vector2(player.transform.position.x - 2, player.transform.position.y + 1), 0.1f);
                }
                else if (player.gameObject.transform.localScale.x < 0)
                {
                    thisObject.transform.DOMove(new Vector2(player.transform.position.x + 2, player.transform.position.y + 1), 0.1f);
                }
            }
            else if(player.gameObject.transform.rotation.z == 0.7071068f || player.gameObject.transform.rotation.z == -0.7071068f)
            {
                if (player.gameObject.transform.localScale.y > 0)
                {
                    thisObject.transform.DOMove(new Vector2(player.transform.position.x - 1, player.transform.position.y -2), 0.1f);
                }
                else if (player.gameObject.transform.localScale.x < 0)
                {
                    thisObject.transform.DOMove(new Vector2(player.transform.position.x -1, player.transform.position.y + 2), 0.1f);
                }
               
            }
        }
       

    }
    public void Suicide()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            isTouch = true;
            player.GetComponent<CharacterManager>().isFollow = true;
        }
    }
}
