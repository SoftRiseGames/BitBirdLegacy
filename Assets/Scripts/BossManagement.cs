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
    public bool retryOne;
    public bool retryTwo;
    public Transform getback;
    public int start;
    void Start()
    {
        StartCoroutine(AttackPointRetryOne());
        StartCoroutine(AttackPointRetryTwo());
    }

    // Update is called once per frame
    void Update()
    {
      
        if (retryOne)
        {
            Instantiate(template[0]);
            retryOne = false;
            StartCoroutine(AttackPointRetryOne());
        }
        if (retryTwo)
        {
            Instantiate(template[1]);
            retryTwo = false;
            StartCoroutine(AttackPointRetryTwo());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            collision.gameObject.transform.position = getback.gameObject.transform.position;
        }
    }

    IEnumerator AttackPointRetryOne()
    {
        yield return new WaitForSeconds(3f);
        retryOne = true;
    }
    IEnumerator AttackPointRetryTwo()
    {
        yield return new WaitForSeconds(2f);
        retryTwo = true;
    }
    
}
