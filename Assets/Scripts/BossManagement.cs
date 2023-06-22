using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagement : MonoBehaviour
{
    public bool isOne;
    public bool isTwo;
    public bool isThree;
    public float randomRange;
    public GameObject[] template;
    public bool retry;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (retry == true)
        {
            randomRange = Random.Range(1, 4);
            retry = false;
            StartCoroutine(AttackPoint());
        }
       
    }

    IEnumerator AttackPoint()
    {
        yield return new WaitForSeconds(1f);
        retry = true;
    }
    
}
