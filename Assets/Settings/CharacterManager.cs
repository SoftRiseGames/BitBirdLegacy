using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
  
    public Rigidbody2D rb;
    float x;
    float y;
    float xRaw;
    float yRaw;
    public float underOffsetValue;
    public float sideOffsetValue;
    public LayerMask groundLayerDetect;
    Collider2D collisionPoint;
    public bool sideRightColliderPoint;
    public bool SideLeftColliderPoint;
    Vector2 underoffset;
    Vector2 sideoffset;

    public float collisionradius;
    Vector2 movementVeriable;
    [SerializeField] float jumpTimer;
    [SerializeField] float jumpStartTimer;
    [SerializeField] TrailRenderer trailRenderer;
    [Header("jump")]
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpForce;
    [SerializeField] float lowJumpMultiplier;
    
    [Header("Walk")]
    [SerializeField] float walkForce;

    [Header("Dash")]
    [SerializeField] float dashForce;
    [SerializeField] float dashTimer;


    [Header("Bools")]
    public bool canJump;
    public bool canWalk;
    public bool isDash;
    public bool dashTimeControl;
    public bool sagsolcont;
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        canWalk = true;
        isDash = false;
        dashTimeControl = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        collisionPoint = Physics2D.OverlapCircle((Vector2)transform.position + underoffset, collisionradius,groundLayerDetect);
        sideRightColliderPoint = Physics2D.OverlapCircle((Vector2)transform.position + sideoffset, collisionradius, groundLayerDetect);
        SideLeftColliderPoint = Physics2D.OverlapCircle((Vector2)transform.position - sideoffset, collisionradius, groundLayerDetect);
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        movementVeriable = new Vector2(x, y);


        ScaleControl();
        GizmoFlipSystem();
        JumpCont();
        GizmoTriggerSystem();
      
       
        if(canWalk)
            Walk(movementVeriable);

       
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isDash)
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimeControl)
            StartCoroutine(Dash(dashTimer));


        if (!canJump && !isDash)
        {
            jumpTimer -= Time.deltaTime;
        }

        
    }

    void GizmoTriggerSystem()
    {
        //yan kontrol
        if (sideRightColliderPoint)
        {
            Debug.Log("sag");
            
        }
        else if (SideLeftColliderPoint)
        {
            Debug.Log("sol");
        }
        ///////////////////////////////////////

        //alt temas kontrolu
        if (collisionPoint)
        {
            if (collisionPoint.gameObject.tag == "ground")
            {
                canJump = true;

                if (!isDash)
                    jumpTimer = jumpStartTimer;
                isDash = false;
                dashTimeControl = true;
            }

        }
        else if (!collisionPoint)
        {
            canJump = false;
        }
        //////////////////////////////
    }
    
    void GizmoFlipSystem()
    {
        if (transform.rotation.z == 0 )
        {
            underoffset = new Vector2(0, underOffsetValue);
            sideoffset = new Vector2(sideOffsetValue, 0);
        }
        else if(transform.rotation.z == -1)
        {
            underoffset = new Vector2(0, -underOffsetValue);
            sideoffset = new Vector2(-sideOffsetValue, 0);
        }
        else if (transform.rotation.z == 0.7071068f)
        {
            underoffset = new Vector2(-underOffsetValue,0);
            sideoffset = new Vector2(0, sideOffsetValue);
        }
        else if(transform.rotation.z == -0.7071068f)
        {
            underoffset = new Vector2(underOffsetValue, 0);
            sideoffset = new Vector2(0, -sideOffsetValue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + underoffset, collisionradius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + sideoffset, collisionradius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere((Vector2)transform.position - sideoffset, collisionradius);
    }
    void Walk(Vector2 movementVeriable)
    {
        if (gameObject.transform.rotation.z == 0)
        {
            rb.velocity = new Vector2(movementVeriable.x * walkForce, rb.velocity.y);
        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementVeriable.x * walkForce);
        }
        else if (gameObject.transform.rotation.z == -1)
        {
            rb.velocity = new Vector2(movementVeriable.x * -walkForce, rb.velocity.y);
        }
        else if (gameObject.transform.rotation.z == -0.7071068f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementVeriable.x * -walkForce);
        }


    }

    void Jump()
    {
        rb.velocity = jumpForce * transform.up;
        Debug.Log(gameObject.transform.position.y);
    }

    void ScaleControl()
    {
        if (x > 0 && transform.localScale.x < 0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
        else if(x<0 && transform.localScale.x>0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
    }


    void JumpCont()
    {
        if (jumpTimer<0 && (transform.rotation.z == 0 || transform.rotation.z == -1) )
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if(jumpTimer<0 && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
        {
            rb.velocity += Vector2.right * Physics2D.gravity.x * (fallMultiplier - 1) * Time.deltaTime;
        }

        
        if (jumpTimer>0 && !Input.GetKey(KeyCode.Space) && (transform.rotation.z == 0 || transform.rotation.z == -1))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
       
        else if (jumpTimer > 0 && !Input.GetKey(KeyCode.Space) && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
        {
            Debug.Log("kontrol");
            rb.velocity += Vector2.right  * Physics2D.gravity.x * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
       
        
    }

    IEnumerator Dash(float dashTimer)
    {
        jumpTimer = 0;
        isDash = true;
        canWalk = false;    
        trailRenderer.enabled = true;
        rb.velocity = Vector2.zero;
      
        rb.gravityScale = 0;
        DashSystem();
        yield return new WaitForSeconds(dashTimer);
        rb.velocity = Vector2.zero;
        trailRenderer.enabled = false;
        dashTimeControl = false;
        jumpTimer -= Time.deltaTime;
        rb.gravityScale = 1;
        canWalk = true;
    }
    void DashSystem()
    {
        Vector2 dir = new Vector2();


        if (gameObject.transform.rotation.z == 0)
        {
            if (xRaw == 0 && yRaw == 0)
                dir = new Vector2(this.gameObject.transform.localScale.x, rb.velocity.y);
            else
                dir = new Vector2(xRaw, yRaw);

           
        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            if (xRaw == 0 && yRaw == 0)
                dir = new Vector2(rb.velocity.y, this.gameObject.transform.localScale.x*1);
            else
                dir = new Vector2(-yRaw, xRaw);
        }
        else if (gameObject.transform.rotation.z == -1)
        {
            if (xRaw == 0 && yRaw == 0)
                dir = new Vector2(-this.gameObject.transform.localScale.x, rb.velocity.y);
            else
                dir = new Vector2(-xRaw, -yRaw);
        }
        else if (gameObject.transform.rotation.z == -0.7071068f)
        {
            if (xRaw == 0 && yRaw == 0)
                dir = new Vector2(rb.velocity.y, -this.gameObject.transform.localScale.x * 1);
            else
                dir = new Vector2(yRaw, -xRaw);
          
        }
        rb.velocity = dir.normalized * dashForce;

    }
    public void CharacterTurn(float gravityX,float gravityY)
    {
         Physics2D.gravity = new Vector2(gravityX, gravityY);
    }
}
