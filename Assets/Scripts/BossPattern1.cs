using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossPattern1 : MonoBehaviour
{
    public Transform[] transforms;
    public int randomLocation;



    void Start()
    {
        if(this.gameObject.tag == "bosspatternfall")
        {
            randomLocation = Random.Range(0, 3);
            this.gameObject.transform.position = new Vector2(transforms[randomLocation].transform.position.x, this.gameObject.transform.position.y);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "bosspatternfall")
        {
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (1 / (0.3f / Time.deltaTime)));
        }
        else if(this.gameObject.tag == "bosspatternvertical")
        {
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - (1 / (0.3f / Time.deltaTime)), this.gameObject.transform.position.y );
        }
       

    }
    
}
