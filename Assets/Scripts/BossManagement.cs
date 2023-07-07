using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagement : MonoBehaviour
{
    public bool isOne;
    public bool isTwo;
    public bool isThree;
    public int randomRange;
    public GameObject[] template;
    public bool retry;
    public Transform getback;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (retry == true)
        {
            randomRange = Random.Range(1, 3);
            Instantiate(template[randomRange-1]);
            retry = false;
            StartCoroutine(AttackPoint());
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            collision.gameObject.transform.position = getback.gameObject.transform.position;
        }
    }

    IEnumerator AttackPoint()
    {
        yield return new WaitForSeconds(1f);
        retry = true;
    }
    
}
