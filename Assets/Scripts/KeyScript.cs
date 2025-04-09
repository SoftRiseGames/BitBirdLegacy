using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeyScript : MonoBehaviour
{
    public GameObject player;
    public KeyScript thisObject;
    public bool isTouch;
    public int controlValue;

    private void Awake()
    {
        if (thisObject == null)
        {
            thisObject = this;
        }
        player = GameObject.Find("player");
        
        if (PlayerPrefs.HasKey(thisObject.name))
        {
            thisObject.controlValue = 1;
        }
        else
        {
            thisObject.controlValue = 0;
        }
    }
    void Start()
    {
        if(thisObject.controlValue == 1)
        {
            thisObject.gameObject.SetActive(false);
        }
        else if(thisObject.controlValue == 0)
        {
            thisObject.gameObject.SetActive(true);
        }
        
    }

    
    void Update()
    {
        
        if (player.GetComponent<CharacterManager>().isFollow && isTouch == true)
        {
            thisObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            thisObject.gameObject.transform.position = player.transform.GetChild(0).transform.position;
            thisObject.gameObject.transform.rotation = player.transform.GetChild(0).transform.rotation;
            thisObject.transform.parent = player.transform;
        }
       

    }
    public void Suicide()
    {
        controlValue = 1;
        PlayerPrefs.SetInt(thisObject.name, controlValue);
        Destroy(thisObject.gameObject);
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
