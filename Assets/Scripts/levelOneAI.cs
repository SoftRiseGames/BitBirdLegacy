using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOneAI : MonoBehaviour
{
    public bool isWalk;
    public Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalk)
        {
            rb.velocity = transform.right * 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "positionCollider")
        {
            isWalk = false;
            rb.velocity = Vector2.zero;
            this.gameObject.transform.position = collision.GetComponent<Transporter>().transporter.position;
        }
    }

    public void eventControltrue()
    {
        isWalk = true;
    }
    public void eventControlfalse()
    {
        isWalk = false;
    }
}
