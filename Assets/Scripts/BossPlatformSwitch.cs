using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformSwitch : MonoBehaviour
{
    public int randomrange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            randomrange = Random.Range(0, 100);
            if(randomrange > 70)
            {
                Debug.Log("a");
            }
            else
            {
                Debug.Log("b");
            }
        }
    }
}
