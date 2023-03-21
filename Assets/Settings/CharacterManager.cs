using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public Animator animator;

    public Rigidbody2D rb;
    float x;
    float y;
    float xRaw;
    float yRaw;
    public float underOffsetValue;
    public float UnderOffsetSideValue;
    
    public float sideOffsetValue;
    public LayerMask groundLayerDetect;
    public bool collisionPoint;
    public bool sideColliderPoint;

    public Vector2 underoffset;
    public Vector2 underSideOffset;
    public Vector2 UnderOffsetSideNegativeOffset;
    Vector2 sideoffset;

    public float collisionSideradius;
    public float collisionGroundradius;
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
    public bool secondJump;
    public bool canWalk;
    public bool canDash;
    public bool sagsolcont;
    bool DashTimerControl;
    bool canCrouch;
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        trailRenderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        canWalk = true;
        canDash = true;

    }

    // Update is called once per frame
    void Update()
    {
        collisionPoint = Physics2D.OverlapCircle((Vector2)transform.position + underoffset, collisionGroundradius, groundLayerDetect) || Physics2D.OverlapCircle((Vector2)transform.position + underSideOffset, collisionGroundradius, groundLayerDetect) || Physics2D.OverlapCircle((Vector2)transform.position + UnderOffsetSideNegativeOffset, collisionGroundradius, groundLayerDetect);
        sideColliderPoint = Physics2D.OverlapCircle((Vector2)transform.position + sideoffset, collisionSideradius, groundLayerDetect) || Physics2D.OverlapCircle((Vector2)transform.position - sideoffset, collisionSideradius, groundLayerDetect);
        
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        movementVeriable = new Vector2(x, y);


        ScaleControl();
        GizmoFlipSystem();
        JumpCont();
        GizmoTriggerSystem();
        Crouch();


        if (canWalk)
            Walk(movementVeriable);


        if (Input.GetKeyDown(KeyCode.Space) && canJump && canDash)
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && !canJump && canDash)
        {
            secondJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && secondJump && canDash)
            DoubleJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash(dashTimer));


        if (!canJump && !DashTimerControl)
        {
            jumpTimer -= Time.deltaTime;
        }

        


    }
    void Crouch()
    {
        if (canCrouch)
        {
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("isCrouch", true);
                canWalk = false;
                canJump = false;
                canDash = false;
            }

            else if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("isCrouch", false);
                canWalk = true;
                canJump = true;
                canDash = true;
            }
        }
    }
    void GizmoTriggerSystem()
    {
        //yan kontrol
        if (sideColliderPoint)
        {

        }
        ///////////////////////////////////////

        //alt temas kontrolu
        if (collisionPoint)
        {  
            canJump = true;
            if (!DashTimerControl)
            {
                jumpTimer = jumpStartTimer;
            }
            
            canDash = true;
            canCrouch = true;
            secondJump = false;
            

        }
        else if (!collisionPoint)
        {
            canJump = false;
            canCrouch = false;
        }
        //////////////////////////////
    }

    void GizmoFlipSystem()
    {
        if (transform.rotation.z == 0)
        {
            underoffset = new Vector2(0, underOffsetValue);
            underSideOffset = new Vector2(UnderOffsetSideValue, underOffsetValue);
            UnderOffsetSideNegativeOffset = new Vector2(-UnderOffsetSideValue, underOffsetValue);
            sideoffset = new Vector2(sideOffsetValue, 0);
        }
        else if (transform.rotation.z == -1)
        {
            underoffset = new Vector2(0, -underOffsetValue);
            underSideOffset = new Vector2(-UnderOffsetSideValue, -underOffsetValue);
            UnderOffsetSideNegativeOffset = new Vector2(UnderOffsetSideValue, -underOffsetValue);
            sideoffset = new Vector2(-sideOffsetValue, 0);
        }
        else if (transform.rotation.z == 0.7071068f)
        {
            underoffset = new Vector2(-underOffsetValue, 0);
            underSideOffset = new Vector2(-underOffsetValue, UnderOffsetSideValue);
            UnderOffsetSideNegativeOffset = new Vector2(-underOffsetValue, -UnderOffsetSideValue);
            sideoffset = new Vector2(0, sideOffsetValue);
        }
        else if (transform.rotation.z == -0.7071068f)
        {
            underoffset = new Vector2(underOffsetValue, 0);
            underSideOffset = new Vector2(underOffsetValue, -UnderOffsetSideValue);
            UnderOffsetSideNegativeOffset = new Vector2(underOffsetValue, UnderOffsetSideValue);
            sideoffset = new Vector2(0, -sideOffsetValue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + underoffset, collisionGroundradius);
        Gizmos.DrawWireSphere((Vector2)transform.position + underSideOffset , collisionGroundradius);
        Gizmos.DrawWireSphere((Vector2)transform.position +UnderOffsetSideNegativeOffset, collisionGroundradius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + sideoffset, collisionSideradius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere((Vector2)transform.position - sideoffset, collisionSideradius);
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
    }
    void DoubleJump()
    {
        if (secondJump)
        {
            jumpTimer = jumpStartTimer;
            rb.velocity = jumpForce * transform.up;
            secondJump = false; 
        }
    }
    void ScaleControl()
    {
        if (x > 0 && transform.localScale.x < 0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
        else if (x < 0 && transform.localScale.x > 0)
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
    }


    void JumpCont()
    {
        if (jumpTimer < 0 && (transform.rotation.z == 0 || transform.rotation.z == -1))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (jumpTimer < 0 && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
        {
            rb.velocity += Vector2.right * Physics2D.gravity.x * (fallMultiplier - 1) * Time.deltaTime;
        }


        if (jumpTimer > 0 && !Input.GetKey(KeyCode.Space) && (transform.rotation.z == 0 || transform.rotation.z == -1))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        else if (jumpTimer > 0 && !Input.GetKey(KeyCode.Space) && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
        {

            rb.velocity += Vector2.right * Physics2D.gravity.x * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }

    IEnumerator Dash(float dashTimer)
    {
       
        canWalk = false;
        
        DashTimerControl = true;
        trailRenderer.enabled = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        jumpTimer = 0;
        DashSystem();
        yield return new WaitForSeconds(dashTimer);
        rb.velocity = Vector2.zero;
        DashTimerControl = false;
        
        trailRenderer.enabled = false;
        canDash = false;
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
                dir = new Vector2(rb.velocity.y, this.gameObject.transform.localScale.x * 1);
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
    public void CharacterTurn(float gravityX, float gravityY)
    {
        Physics2D.gravity = new Vector2(gravityX, gravityY);
    }
}