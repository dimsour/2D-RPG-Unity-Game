using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveScript : MonoBehaviour
{

    public float runSpeed;
    public float force;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isGrounded = false;
    public bool swordhit1;
    float speed, shieldSpeed;
    public Transform groundCheck; // A position marking where to check if the player is grounded.
    private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.
    public Transform ceilingCheck; // A position marking where to check for ceilings
    private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public AudioSource swordHit;
    public AudioSource moveSound, jumpSound;
    public int randomNum, randomTemp;
    public AudioClip[] jumpClips;
    [SerializeField]
    private LayerMask whatIsGround; // A mask determining what is ground to the character
    public bool right, left, shield, hit, jump;
    private bool canHit = true;
    bool rightKeyDown, leftKeyDown, hitDown, shieldDown, jumpDown;
    buttonsScript ButtonScriptRight, ButtonScriptLeft,ButtonScriptHit,ButtonScriptJump,ButtonScriptShield;


    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shieldSpeed = runSpeed / 3;
        ButtonScriptRight = GameObject.Find("rightButton").GetComponent<buttonsScript>();
        ButtonScriptLeft = GameObject.Find("leftButton").GetComponent<buttonsScript>();
        ButtonScriptHit = GameObject.Find("hitButton").GetComponent<buttonsScript>();
        ButtonScriptJump = GameObject.Find("jumpButton").GetComponent<buttonsScript>();
        ButtonScriptShield = GameObject.Find("shieldButton").GetComponent<buttonsScript>();


        //groundCheck = transform.Find("GroundCheck");
        //ceilingCheck = transform.Find("CeilingCheck");
    }
    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);
 

    }
    void Update()
    {
        rightKeyDown = ButtonScriptRight.rightKeyDown;
        leftKeyDown = ButtonScriptLeft.leftKeyDown;
        hitDown = ButtonScriptHit.hitDown;
        //shieldDown = ButtonScriptShield.shieldDown;
        jumpDown = ButtonScriptJump.jumpDown;

        if ((Input.GetKey(KeyCode.RightArrow)) || rightKeyDown || (Input.GetKey(KeyCode.RightArrow) && !isGrounded)) //movement // can move while shield and jumped
        {
            moveRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || leftKeyDown|| (Input.GetKey(KeyCode.LeftArrow) && !isGrounded)) //movement // can move while shield and jumped
        {
            moveLeft();
        }
        if ((Input.GetKeyDown(KeyCode.C)|| hitDown)&& !swordhit1 && canHit) // swordhit
        {
            swordHitVoid();
            canHit = false;
            StartCoroutine(CanHitDelay());
        }


        if ((Input.GetKeyDown(KeyCode.Space)|| jumpDown) && !Input.GetKey(KeyCode.X)) //jump 
        {
            jumpVoid();
        }


        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||  !leftKeyDown && !rightKeyDown) //stop runing animation
            {
            anim.SetBool("running", false);
        }


        if(Input.GetKey(KeyCode.X)||shieldDown) //shield animation
        {
            shieldVoid();
        }


        if(Input.GetKeyUp(KeyCode.X)||!shieldDown)//stop shield
        {
            anim.SetBool("shield", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && (Input.GetKey(KeyCode.X))|| Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.X))) // when shield an run get half speed
        {
            if(isGrounded)
            speed = shieldSpeed;
        }
        else
            speed = runSpeed;

        if (!isGrounded)
            speed = runSpeed / 1.25f;
    }
        
        IEnumerator stop()
    {
        yield return new WaitForSeconds(0.230f); //wait for animation to finish
        anim.SetBool("swordHit1", false);
        swordhit1 = false;
    }
    IEnumerator CanHitDelay()
    {
        yield return new WaitForSeconds(0.5f); //wait for animation to finish
        canHit = true;
    }

    void jumpSoundVoid()
    {
        randomNum = Random.RandomRange(0, 3);
        while (randomNum == randomTemp) // so it wont play same clip twice
        {
            randomNum = Random.RandomRange(0, 3);
        }
            jumpSound.clip = jumpClips[randomNum];
            randomTemp = randomNum;
            jumpSound.Play();
    }
    public void moveRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.localScale = new Vector3(1, 1, 1);
        if (isGrounded)
        {
            anim.SetBool("running", true);
        }
    }
    public void moveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        transform.localScale = new Vector3(-1, 1, 1);
        if (isGrounded)
        {
            anim.SetBool("running", true);
        }
    }
    public void swordHitVoid()
    {
        swordhit1 = true;
        swordHit.Play();
        anim.SetBool("swordHit1", true);
        StartCoroutine(stop());
    }
    public void jumpVoid()
    {
        if (isGrounded)
        {
            jumpSoundVoid();
            rb.AddRelativeForce(transform.up * force * 10);
            ButtonScriptJump.jumpDown = false;
        }
    }
    public void shieldVoid()
    {
        anim.SetBool("shield", true);
    }
}

