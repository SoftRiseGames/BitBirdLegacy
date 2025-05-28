using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using System;
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
    float jumpTimer;
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

    [TabGroup("Tramboline")]
    public float TrambolineSpeed;
    [TabGroup("Tramboline")]
    public float TrambolineDuration;

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
    public GameObject InGameCamera;

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
    public ParticleSystem DustParticle;
    public ParticleSystem DashParticle;

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
    Coroutine dashCoroutine;
    Coroutine TrambolineCoroutine;
    bool isTramboline;

    private float startingMass;
    public PlayerInput playerInput;

    public static Action isGround;
    public static Action isDeathEvent;
    bool isBreakableGround;

    [SerializeField] GameObject DashButtonTutorial;

    int dashUnlockInteger;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("DashUnlocked"))
            dashUnlockInteger = 1;
        else
            dashUnlockInteger = 0;
    }
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        trailRenderer.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        startingMass = rb.mass;
        canWalk = true;
        canDash = true;
        FallTimerControl = true;
        StartPrefs();

        if (dashUnlockInteger == 1)
            dashControl = true;
        else
            dashControl = false;



    }
    private void OnEnable()
    {
        kapi.isMove += isCharacterMove;
        kapi.isNonMove += isCharacterNonMove;
    }
    private void OnDisable()
    {
        kapi.isMove -= isCharacterMove;
        kapi.isNonMove -= isCharacterNonMove;
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

       

        x = playerInput.actions["Move"].ReadValue<Vector2>().x;
        y = playerInput.actions["Move"].ReadValue<Vector2>().y;


        movementVeriable = new Vector2(x, y);

        anims();
        ScaleControl();
        GizmoFlipSystem();

        GizmoTriggerSystem();
        GizmoTriggerSystem();
        //Crouch();
        gravity();

        coyoteAndFall();
        coyoteControl();

        ManageWalk();

        DashAndTrambolineControlUpdater();



        if (canWalk)
        {
            Walk(movementVeriable);
        }



        if (NormalGravity)
        {
            JumpGravity();
            JumpCont();
            rb.mass = startingMass;
        }


        if (playerInput.actions["Jump"].WasPressedThisFrame() && coyoteTimeCounter > 0f && canDash && !DashTimerControl && canJump)
        {

            Jump();
            jumpEffect();
        }


        if (playerInput.actions["Jump"].WasPressedThisFrame() && secondJump && !canJump && canDash && doubleJumpControl)
            DoubleJump();

        if (playerInput.actions["Dash"].WasPerformedThisFrame() && canDash && dashControl)
        {
            xRaw = playerInput.actions["DashAxis"].ReadValue<Vector2>().x;
            yRaw = playerInput.actions["DashAxis"].ReadValue<Vector2>().y;

           
            xRaw = (xRaw > 0) ? (xRaw > 0.5f ? 1 : 0) : (xRaw < -0.5f ? -1 : 0);
            yRaw = (yRaw > 0) ? (yRaw > 0.5f ? 1 : 0) : (yRaw < -0.5f ? -1 : 0);


            dashCoroutine = StartCoroutine(Dash(dashTimer));
            DashEffect();
        }
    }

    void isCharacterMove()
    {
        
        canWalk = true;
        canJump = true;
        canDash = true;
    }
    void isCharacterNonMove()
    {
        rb.velocity = Vector2.zero;
        canWalk = false;
        canJump = false;
        canDash = false;
    }
    void coyoteControl()
    {
        if (coyoteTimeCounter > 0f)
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
            rb.gravityScale = 1.7f;
        else
            rb.gravityScale = 3;

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
                this.gameObject.transform.SetParent(collisionPoint.transform, true);

            if (collisionPoint.gameObject.tag == "Breakable")
            {
                collisionPoint.GetComponent<GoundAnimated>().AnimatorStarted();
                isBreakableGround = true;
            }
               

           if(collisionPoint.gameObject.tag != "Breakable" && isBreakableGround == true)
            {
                isBreakableGround = false;
                isGround.Invoke();
            }

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
        animator.SetBool("isDash", true);
        /*
        // Hedef �l�ekler belirliyoruz.
        Vector2 targetScale = transform.localScale.x >= 0 ? new Vector2(19.2f, 7.2f) : new Vector2(-19.2f, 7.2f);

        // �lk animasyonu ba�lat�yoruz.
        transform.DOScale(targetScale, 0.1f)
            .OnUpdate(UpdateColliderSize)
            .OnComplete(() =>
            {
                Vector2 finalScale = targetScale.x > 0 ? new Vector2(12f, 12f) : new Vector2(-12f, 12f);
                // �kinci animasyonu ba�lat�yoruz.
                transform.DOScale(finalScale, 0.1f).OnUpdate(UpdateColliderSize);
            });
        */



    }


    void anims()
    {
        if (transform.rotation.z == 0)
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
                animator.SetBool("isJump", false);
                animator.SetBool("isfall", true);
            }
            else if (rb.velocity.y > 0 && !collisionPoint)
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



        else if (transform.rotation.z == 0.7071068f)
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
            else if (rb.velocity.x < 0 && !collisionPoint)
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
        if (movementVeriable.x > 0)
            movementVeriable.x = 1;
        else if (movementVeriable.x < 0)
            movementVeriable.x = -1;
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

        if (collision.gameObject.tag == "killer")
        {
            if (dashCoroutine != null)
                StopCoroutine(dashCoroutine);
            rb.velocity = Vector2.zero;

            dashTimer = 0;
            canDash = false;
            isDead = true;
            VoiceManager.instance.SFXSoundPlay("Death");
            rb.gravityScale = 0;
            canJump = false;
            NormalGravity = false;
            canWalk = false;
            animator.SetBool("isDeath", true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "savepoint")
        {
            if(collision.GetComponent<SaverObjectList>().isTouched == 0)
            {
                PlayerPrefs.SetFloat("gravityX", beginningGravityX);
                PlayerPrefs.SetFloat("gravityY", beginningGravityy);
                PlayerPrefs.SetFloat("rotation", rotationz);

                begininngPositionX = collision.gameObject.transform.position.x;
                beginningPositionY = collision.gameObject.transform.position.y;

                PlayerPrefs.SetFloat("positionX", begininngPositionX);
                PlayerPrefs.SetFloat("positionY", beginningPositionY);

                collision.GetComponent<SaverObjectList>().isTouchVoid();

            }
        }
        if(collision.gameObject.tag == "DashUnlock")
        {
            dashControl = true;
            dashUnlockInteger = 1;
            PlayerPrefs.SetInt("DashUnlocked", dashUnlockInteger);
            DashButtonTutorial.SetActive(true);
        }
        if (collision.gameObject.name == "DemoTrigger")
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    
   



    void ManageWalk()
    {
        if ((rb.velocity.x > 0 && x > 0) || (rb.velocity.x < 0 && x < 0))
        {
            aktifhiz = Mathf.Lerp(aktifhiz, walkForce, walkcarpan * Time.deltaTime);

        }
        else if (x != 0)
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
        VoiceManager.instance.SFXSoundPlay("Jump");
        createdust();
        rb.velocity = jumpForce * transform.up;

    }
    public void DoubleJump()
    {
        if (secondJump)
        {
            StartCoroutine(DoubleJumpWait());
            rb.velocity = (jumpForce / 2) * transform.up;
            secondJump = false;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camlimit")
        {
            InGameCamera = null;
        }
        if (collision.gameObject.tag == "DashUnlock")
        {
            DashButtonTutorial.SetActive(false);
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camlimit")
        {

            InGameCamera = collision.gameObject.transform.GetChild(0).transform.gameObject;

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


        if (jumpTimer > 0 && !playerInput.actions["Jump"].IsPressed() && (transform.rotation.z == 0 || transform.rotation.z == -1))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


        else if (jumpTimer > 0 && !playerInput.actions["Jump"].IsPressed() && (transform.rotation.z == 0.7071068f || transform.rotation.z == -0.7071068f))
        {

            rb.velocity += Vector2.right * Physics2D.gravity.x * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }
    public void DeathSquence()
    {
        isDead = true;
        rb.gravityScale = 0;
        canJump = false;
        NormalGravity = false;
        canWalk = false;
        rb.velocity = Vector2.zero;
    }

    void DashAndTrambolineControlUpdater()
    {
        if (DashTimerControl && TrambolineCoroutine != null)
        {
            StopCoroutine(TrambolineCoroutine);
        }
    }
    public IEnumerator Dash(float dashTimer)
    {

        NormalGravity = false;
        canWalk = false;
        DashTimerControl = true;
        trailRenderer.enabled = true;
        CreateDashParticle();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        jumpTimer = 0;
        rb.mass = 0;
        DashSystem();
        yield return new WaitForSeconds(dashTimer);
        NormalGravity = true;
        CharacterBaseMode();
    }

    void CharacterBaseMode()
    {
        rb.velocity = Vector2.zero;
        DashTimerControl = false;
        trailRenderer.enabled = false;
        animator.SetBool("isDash", false);
        canDash = false;
        canWalk = true;
    }
    void DashSystem()
    {
        Vector2 dir = new Vector2();
        CameraShake.instance.DashShake();

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
        rb.velocity = dir.normalized * dashForce * Time.fixedDeltaTime;



    }

    public void Restart()
    {
        isDeathEvent.Invoke();
    }

    void gravity()
    {
        if (gameObject.transform.rotation.z == 0)
        {
            CharacterTurn(0, -9.8f);
        }
        else if (gameObject.transform.rotation.z == 0.7071068f)
        {
            CharacterTurn(9.8f, 0);
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

    public IEnumerator TrambolineAddForce(Transform transform, float AxisValue)
    {
        NormalGravity = false;
        rb.gravityScale = 0;
        isTramboline = true;
        float force = TrambolineSpeed;
        float dampingDuration = TrambolineDuration;
      


        CameraShake.instance.TrambolineShake();


        if (dashCoroutine != null)
            StopCoroutine(dashCoroutine);

        rb.velocity = Vector2.zero;

        CharacterBaseMode();

        if (isTramboline)
        {
            canWalk = false;
            rb.velocity = transform.up * force * Time.fixedDeltaTime;
            aktifhiz = 0;
        }

        TrambolineCoroutine = StartCoroutine(DampenVelocity(dampingDuration));

        // H�z� kademeli olarak azaltan coroutine ba�lat�l�yor
        yield return TrambolineCoroutine;


    }

    private IEnumerator DampenVelocity(float duration)
    {
        Debug.Log(InGameCamera.transform.rotation.z);
        Debug.Log(InGameCamera.transform.eulerAngles.z);
        float elapsedTime = 0f;
        Vector2 initialVelocity = rb.velocity;
        canDash = true;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.fixedDeltaTime;

            if (!DashTimerControl)
                rb.velocity = initialVelocity * (1 - elapsedTime / duration);


            if (rotationz == 0 || Mathf.Abs(rotationz) == 180)
            {
                if ((collisionPoint) || Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
                    canWalk = false;
                else
                    canWalk = true;
            }
            else if (Mathf.Abs(rotationz) == 90 || Mathf.Abs(rotationz) == 270)
            {
                if ((collisionPoint) || Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x))
                    canWalk = false;
                else
                    canWalk = true;
            }

            yield return new WaitForFixedUpdate();
        }

        if (elapsedTime >= duration)
        {
            if (!DashTimerControl)
            {
                isTramboline = false;
                NormalGravity = true;
                canWalk = true;
            }

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
            DustParticle.Play();
        }
    }

    void CreateDashParticle()
    {
        if (!isDead)
        {
            DashParticle.Play();
        }
    }

}