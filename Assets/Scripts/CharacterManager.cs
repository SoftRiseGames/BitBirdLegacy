using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
public class CharacterManager : MonoBehaviour
{
    [TabGroup("Layers")]
    public LayerMask groundLayerDetect;
    [TabGroup("Layers")]
    public LayerMask sideLayerDedect;

    [TabGroup("CollisionSetup")]
    public Collider2D collisionPoint;
    [TabGroup("CollisionSetup")]
    public float collisionSideradius;
    [TabGroup("CollisionSetup")]
    public float collisionGroundradius;
    [TabGroup("CollisionSetup")]
    public Vector2 underoffset;
    
    [TabGroup("CollisionSetup")]
    public float underOffsetValue;
    [TabGroup("CollisionSetup")]
    Vector2 sideoffset;
    [TabGroup("CollisionSetup")]
    public float sideOffsetValue;
    [TabGroup("CollisionSetup")]
    public bool sideColliderPoint;

    [TabGroup("jump")]
    [SerializeField] float fallMultiplier;
    [TabGroup("jump")]
    [SerializeField] float jumpForce;
    [TabGroup("jump")]
    [SerializeField] float lowJumpMultiplier;
    [TabGroup("jump")]
    [SerializeField] float jumpTimer;
    [TabGroup("jump")]
    [SerializeField] float jumpStartTimer;
    [TabGroup("jump")]
    [SerializeField] float coyoteTime;
    [TabGroup("jump")]
    private float coyoteTimeCounter;

    [TabGroup("Walk")]
    public Vector2 movementVeriable;
    [TabGroup("Walk")]
    [SerializeField] float walkForce;
    [TabGroup("Walk")]
    [SerializeField] float walkcarpan;
    [TabGroup("Walk")]
    [SerializeField] float durmacarpan;
    [TabGroup("Walk")]
    [SerializeField] float aktifhiz;
    float x;
    float y;

    [TabGroup("Dash")]
    [SerializeField] float dashForce;
    [TabGroup("Dash")]
    public float dashTimer;
    float xRaw;
    float yRaw;

    [TabGroup("Feels")]
    public float yurumeamplitude;
    [TabGroup("Feels")]
    public float ziplamaamplitude;
    [TabGroup("Feels")]
    public float yurumeduration;
    [TabGroup("Feels")]
    public float ziplamaduration;
    [TabGroup("Feels")]
    public inCameraSettings cameraShake;

    [TabGroup("Bools")]
    public bool canJump;
    [TabGroup("Bools")]
    public bool secondJump;
    [TabGroup("Bools")]
    public bool canWalk;
    [TabGroup("Bools")]
    public bool canDash;
    [TabGroup("Bools")]
    public bool sagsolcont;
    [TabGroup("Bools")]
    public bool camrotate = false;
    [TabGroup("Bools")]
    public bool DashTimerControl;
    [TabGroup("Bools")]
    bool canCrouch;
    [TabGroup("Bools")]
    public bool isFollow;
    [TabGroup("Bools")]
    public bool RotationDedection;
    [TabGroup("Bools")]
    bool isDead;
    [TabGroup("Bools")]
    public bool NormalGravity;


    [TabGroup("ControlBools")]
    public bool doubleJumpControl;
    [TabGroup("ControlBools")]
    public bool dashControl;
    [TabGroup("ControlBools")]
    public bool FallTimerControl;


    [TabGroup("SavedGravities")]
    public float beginningGravityX;
    [TabGroup("SavedGravities")]
    public float beginningGravityy;

    [TabGroup("SavedPosition")]
    float begininngPositionX;
    [TabGroup("SavedPosition")]
    float beginningPositionY;
    
    [TabGroup("Other")]
    public Animator animator;
    [TabGroup("Other")]
    public ParticleSystem dust;
    [TabGroup("Other")]
    public Rigidbody2D rb;
    [TabGroup("Other")]
    public float rotationz;
    [TabGroup("Other")]
    public GameObject platforms;
    [TabGroup("Other")]
    [SerializeField] TrailRenderer trailRenderer;


    public bool left90;
    public bool right90;
 
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        
        trailRenderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        canWalk = true;
        canDash = true;
        FallTimerControl = true;

       
       
        StartPrefs();
    }
    void StartPrefs()
    {
        if (PlayerPrefs.HasKey("gravityX") && PlayerPrefs.HasKey("gravityY"))
        {
            beginningGravityX = PlayerPrefs.GetFloat("gravityX");
            beginningGravityy = PlayerPrefs.GetFloat("gravityY");
            Physics2D.gravity = new Vector2(beginningGravityX, beginningGravityy);
        }
        else
        {
            beginningGravityX = 0;
            beginningGravityy = -9.8f;
            Physics2D.gravity = new Vector2(beginningGravityX, beginningGravityy);
        }

        if (PlayerPrefs.HasKey("rotation"))
        {
            rotationz = PlayerPrefs.GetFloat("rotation");
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationz);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (PlayerPrefs.HasKey("positionX") && PlayerPrefs.HasKey("positionY"))
        {
            this.gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("positionX"), PlayerPrefs.GetFloat("positionY"));
        }
        else
        {
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        }
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
       
        GizmoTriggerSystem();
        //Crouch();
        gravity();
        ManageWalk();
        coyoteAndFall();
        coyoteControl();
       
        if (canWalk)
            Walk(movementVeriable);
        if (NormalGravity)
        {
            JumpGravity();
            JumpCont();
        }
            

        if (Input.GetButtonDown("jump") && coyoteTimeCounter>0f && canDash && !DashTimerControl && canJump)
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
            
    
        

    }

    void coyoteControl()
    {
        if(coyoteTimeCounter > 0f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
       
    }
    void coyoteAndFall()
    {
        if (canJump)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (FallTimerControl)
        {
            if (!canJump && !DashTimerControl && !isDead)
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
    void JumpGravity()
    {
        
        if (canJump)
            rb.gravityScale = 1;
        else
            rb.gravityScale =3;
        
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
        if (collisionPoint && !isDead)
        {  
            canJump = true;
            if (!DashTimerControl)
            {
                jumpTimer = jumpStartTimer;
            }
          
            canDash = true;
            canCrouch = true;
            secondJump = true;

            if (collisionPoint.gameObject.tag == "platform")
                this.gameObject.transform.SetParent(collisionPoint.transform,true); 


        }
        else if (!collisionPoint && !isDead)
        {
       
            canJump = false;
            canCrouch = false;
            this.gameObject.transform.SetParent(null);
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

       
    }


    void anims()
    {
        if (transform.rotation.z == 0 )
        {
            if((x>0 || x<0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
                //animator.SetBool("isfall", false);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }
            if((rb.velocity.y<0 ) && !collisionPoint)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", true);
            }
            else if(rb.velocity.y > 0 && !collisionPoint)
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isfall", false);
            }
           
            else
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", false);
            }
        }
        if (transform.rotation.z == -1)
        {
            if ((x > 0 || x < 0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
                //animator.SetBool("isfall", false);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }
            if ((rb.velocity.y < 0) && !collisionPoint)
            {
                Debug.Log("a");
                animator.SetBool("isJump", true);
                animator.SetBool("isfall", false);
            }
            else if (rb.velocity.y > 0 && !collisionPoint)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", true);
            }

            else
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", false);
            }
        }



        else if (transform.rotation.z == 0.7071068f )
        {

            if ((x > 0 || x < 0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
               // animator.SetBool("isfall", false);
            }

            else
            {
                animator.SetBool("isWalk", false);
            }

            if ((rb.velocity.x > 0) && !collisionPoint)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", true);
            }
            else if(rb.velocity.x<0 && !collisionPoint)
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isfall", false);
            }
            else
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", false);
            }
            
        }
        else if (transform.rotation.z == -0.7071068f)
        {
            if ((x > 0 || x < 0) && collisionPoint)
            {
                animator.SetBool("isWalk", true);
                // animator.SetBool("isfall", false);
            }

            else
            {
                animator.SetBool("isWalk", false);
            }

            if ((rb.velocity.x > 0) && !collisionPoint)
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isfall", false);
            }
            else if (rb.velocity.x < 0 && !collisionPoint)
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", true);
            }
            else
            {
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", false);
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
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "killer")
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            canJump = false;
            
            jumpTimer = 0;
            canWalk = false;
            animator.SetBool("isDeath", true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
      

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "savepoint")
        {
           
            PlayerPrefs.SetFloat("gravityX", beginningGravityX);
            PlayerPrefs.SetFloat("gravityY", beginningGravityy);
            PlayerPrefs.SetFloat("rotation", rotationz);

            begininngPositionX = collision.gameObject.transform.position.x;
            beginningPositionY = collision.gameObject.transform.position.y;

            PlayerPrefs.SetFloat("positionX", begininngPositionX);
            PlayerPrefs.SetFloat("positionY", beginningPositionY);
        }
        //Ogem demo sonrasý deðiþecek,silinecek
        if (collision.gameObject.name == "DemoTrigger")
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        createdust();
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
        {
            createdust();
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
        }
        else if (x < 0 && transform.localScale.x > 0)
        {
            createdust();
            this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x * -1, this.gameObject.transform.localScale.y);
        }
          
        
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


        if (jumpTimer > 0 && !Input.GetButton("jump") && (transform.rotation.z == 0 || transform.rotation.z == -1))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        else if (jumpTimer > 0 && !Input.GetButton("jump") && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
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
        rb.velocity = dir.normalized * dashForce*Time.fixedDeltaTime;
        

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

    void createdust()
    {
        if (!isDead)
        {
            dust.Play();
        }
    }

}