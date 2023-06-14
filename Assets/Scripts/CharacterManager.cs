using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class CharacterManager : MonoBehaviour
{

    public Animator animator;

    public Rigidbody2D rb;
    float x;
    float y;
    float xRaw;
    float yRaw;
    public float underOffsetValue;
    public float rotationz;

    public float sideOffsetValue;
    public LayerMask groundLayerDetect;

    public LayerMask sideLayerDedect;
    public Collider2D collisionPoint;

    public bool sideColliderPoint;
    public GameObject platforms;
    public Vector2 underoffset;
    public Vector2 underSideOffset;
    public inCameraSettings cameraShake;
    Vector2 sideoffset;

    public float collisionSideradius;
    public float collisionGroundradius;
    public Vector2 movementVeriable;
    [SerializeField] float jumpTimer;
    [SerializeField] float jumpStartTimer;
    [SerializeField] TrailRenderer trailRenderer;
    [Header("jump")]
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpForce;
    [SerializeField] float lowJumpMultiplier;

    [Header("Walk")]
    [SerializeField] float walkForce;
    [SerializeField] float walkcarpan;
    [SerializeField] float durmacarpan;
    [SerializeField] float aktifhiz;

    [Header("Dash")]
    [SerializeField] float dashForce;
    public float dashTimer;
    private float coyoteTimeCounter;
    [SerializeField] float coyoteTime;
    

    [Header("Titresim")]
    public float yurumeamplitude;
    public float ziplamaamplitude;
    public float yurumeduration;
    public float ziplamaduration;

    [Header("Bools")]
    public bool canJump;
    public bool secondJump;
    public bool canWalk;
    public bool canDash;
    public bool sagsolcont;
    public bool camrotate = false;
    public bool DashTimerControl;
    bool canCrouch;
    public bool isFollow;
    public bool RotationDedection;
    [Header("ControlBools")]
    public bool doubleJumpControl;
    public bool dashControl;
  
    bool FallTimerControl;
    [Header("BeginningGravities")]
    public float beginningGravityX;
    public float beginningGravityy;

    [Header("BeginningPosition")]

    float begininngPositionX;
    float beginningPositionY;
    
   
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        
        trailRenderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        canWalk = true;
        canDash = true;

        if(PlayerPrefs.HasKey("GravityX") && PlayerPrefs.HasKey("GravityY"))
        {
            Physics2D.gravity = new Vector2(PlayerPrefs.GetFloat("GravityX"), PlayerPrefs.GetFloat("GravityY"));
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
        
        if (PlayerPrefs.HasKey("rotation"))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, PlayerPrefs.GetFloat("rotation"));
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(PlayerPrefs.HasKey("positionX") && PlayerPrefs.HasKey("positionY"))
        {
            this.gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
        }
        else
        {
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        }
        FallTimerControl = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        collisionPoint = Physics2D.OverlapCircle((Vector2)transform.position + underoffset, collisionGroundradius, groundLayerDetect);
        
        sideColliderPoint = Physics2D.OverlapCircle((Vector2)transform.position + sideoffset, collisionSideradius, sideLayerDedect);
        
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        movementVeriable = new Vector2(x, y);

        anims();
        ScaleControl();
        GizmoFlipSystem();
        JumpCont();
        GizmoTriggerSystem();
        //Crouch();
        //gravity();
        ManageWalk();
  
        if (canWalk)
            Walk(movementVeriable);

        if (canJump)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter>0f && canDash && !DashTimerControl)
        {
            Jump();
            jumpEffect();
        }
            

        if (Input.GetKeyDown(KeyCode.Space) && secondJump && !canJump && canDash &&doubleJumpControl)
            DoubleJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashControl)
        {
            StartCoroutine(Dash(dashTimer));
            DashEffect();
        }
            
    
        if (FallTimerControl)
        {
            if (!canJump && !DashTimerControl)
            {
                jumpTimer -= Time.deltaTime;
            }
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
            if (transform.rotation.z == 0 || transform.rotation.z == -1)
            {
                if (rb.velocity.x < 0 || rb.velocity.x > 0)
                    Debug.Log("ittirme");
            }
           
            else if (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f)
            {
                if (rb.velocity.y < 0 || rb.velocity.y > 0)
                    Debug.Log("ittirme");
            }
           
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
            secondJump = true;

            if(collisionPoint.gameObject.tag == "platform")
                this.gameObject.transform.parent = collisionPoint.transform;


        }
        else if (!collisionPoint)
        {
       
            canJump = false;
            canCrouch = false;
            this.gameObject.transform.parent = null;
        }

        
        //////////////////////////////
    }

  
    public void DashEffect()
    {

        if (gameObject.transform.localScale.x >= 0)
        {
            transform.DOScale(new Vector2(19.2f, 7.2f), 0.1f).OnComplete(() => transform.DOScale(new Vector2(12f, 12f), 0.1f));
        }
        else if (gameObject.transform.localScale.x < 0)
        {
            transform.DOScale(new Vector2(-19.2f, 7f), 0.1f).OnComplete(() => transform.DOScale(new Vector2(-12f, 12f), 0.1f));
        }

        cameraShake.shake(ziplamaamplitude, ziplamaduration);
    }


    void anims()
    {
        if (transform.rotation.z == 0 || transform.rotation.z == -1)
        {
            if((x>0 || x<0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
            }
            
            else
            {
                animator.SetBool("isWalk", false);
            }
            
            if((rb.velocity.y>0 || rb.velocity.y<0) && !collisionPoint)
            {
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }
        }
       
        else if (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f)
        {

            if ((x > 0 || x < 0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
            }

            else
            {
                animator.SetBool("isWalk", false);
            }

            if ((rb.velocity.x > 0 || rb.velocity.x < 0) && !collisionPoint)
            {
                animator.SetBool("isJump", true);
            }
            else
            {
                animator.SetBool("isJump", false);
            }
        }
        
    }
    void GizmoFlipSystem()
    {
        
        if (transform.rotation.z == 0)
        {
            underoffset = new Vector2(0, underOffsetValue);
            sideoffset = new Vector2(sideOffsetValue, 0);
        }
        else if (transform.rotation.z == -1)
        {
            underoffset = new Vector2(0, -underOffsetValue);
            sideoffset = new Vector2(-sideOffsetValue, 0);
        }
        else if (transform.rotation.z == 0.7071068f)
        {
            underoffset = new Vector2(-underOffsetValue, 0);
            sideoffset = new Vector2(0, sideOffsetValue);
        }
        else if (transform.rotation.z == -0.7071068f)
        {
            underoffset = new Vector2(underOffsetValue, 0);
            sideoffset = new Vector2(0, -sideOffsetValue);
        }

        if (this.gameObject.transform.localScale.x > 0 && sideOffsetValue < 0)
        {
            sideoffset = sideoffset * -1;
        }
        else if (this.gameObject.transform.localScale.x < 0 && sideOffsetValue > 0)
        {
            sideoffset = sideoffset * -1;
        }


    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + underoffset, collisionGroundradius);
       
      
       
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + sideoffset, collisionSideradius);
    }
    public void Walk(Vector2 movementVeriable)
    {
        if (gameObject.transform.rotation.z == 0)
        {
            rb.velocity = new Vector2(movementVeriable.x * aktifhiz, rb.velocity.y);

        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementVeriable.x * aktifhiz);

        }
        else if (gameObject.transform.rotation.z == -1)
        {
            rb.velocity = new Vector2(movementVeriable.x * -aktifhiz, rb.velocity.y);

        }
        else if (gameObject.transform.rotation.z == -0.7071068f)
        {
            rb.velocity = new Vector2(rb.velocity.x, movementVeriable.x * -aktifhiz);
        }
        /*
        if (x > 0 || x<0 && collisionPoint)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "killer")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "savepoint")
        {
            Debug.Log("a");
            PlayerPrefs.SetFloat("gravityX", beginningGravityX);
            PlayerPrefs.SetFloat("gravityY", beginningGravityy);
            PlayerPrefs.SetFloat("rotation", rotationz);

            begininngPositionX = collision.gameObject.transform.position.x;
            beginningPositionY = collision.gameObject.transform.position.y;

            PlayerPrefs.SetFloat("positionX", begininngPositionX);
            PlayerPrefs.SetFloat("positionY", beginningPositionY);
        }
    }


    void ManageWalk()
    {
        if((rb.velocity.x>0 && x> 0) || (rb.velocity.x<0 && x<0))
        {
            aktifhiz = Mathf.Lerp(aktifhiz, walkForce, walkcarpan * Time.deltaTime);
        }
        else if(x != 0)
        {
            aktifhiz = Mathf.Lerp(aktifhiz, walkForce, durmacarpan * Time.deltaTime);
        }
        else
        {
            aktifhiz = Mathf.Lerp(aktifhiz, 0, durmacarpan * Time.deltaTime);
        }
    }


    public void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = jumpForce * transform.up;
    }
    public void DoubleJump()
    {
        if (secondJump)
        {
            StartCoroutine(DoubleJumpWait());
            rb.velocity = jumpForce * transform.up;
            secondJump = false; 
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camlimit")
        {
            cameraShake = null;
        }
       
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "camlimit")
        {
           
            cameraShake = collision.gameObject.transform.GetChild(0).GetComponent<inCameraSettings>();
            
        }

    }
    public void jumpEffect()
    {
        
        //StartCoroutine(jumpEffectwait());
        /*
        if (collisionPoint)
        {
            SpriteRenderer sprites = GetComponent<SpriteRenderer>();
            if (gameObject.transform.localScale.x > 0)
            {
                
                transform.DOScale(new Vector2(7.2f, 19.2f), 0.1f).OnComplete(() =>  transform.DOScale(new Vector2(12f, 12f), 0.1f));

            }
            else if (gameObject.transform.localScale.x < 0)
            {

                transform.DOScale(new Vector2(-7.2f, 19.2f), 0.1f).OnComplete(() => transform.transform.DOScale(new Vector2(-12f, 12f), 0.1f));
            }
           
        }
        */
        
        //cameraShake.shake(ziplamaamplitude, ziplamaduration);
    }

  
    IEnumerator DoubleJumpWait()
    {
       
        FallTimerControl = false;
        jumpTimer = jumpStartTimer;
        yield return new WaitForSeconds(.1f);
        FallTimerControl = true;
        if (canDash)
        {
            canDash = false;
            yield return new WaitForSeconds(0.01f);
            canDash = true;
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

   public IEnumerator Dash(float dashTimer)
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

   
   void gravity()
    {
        if (gameObject.transform.rotation.z == 0)
        {
            CharacterTurn(0, -9.8f);
        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            CharacterTurn(9.8f,0);
        }
        else if (gameObject.transform.rotation.z == -1)
        {
            CharacterTurn(0, 9.8f);
        }
        else if (gameObject.transform.rotation.z == -0.7071068f)
        {
            CharacterTurn(-9.8f, 0);
        }
    }
    public void CharacterTurn(float gravityX, float gravityY)
    {
        Physics2D.gravity = new Vector2(gravityX, gravityY);
    }

}