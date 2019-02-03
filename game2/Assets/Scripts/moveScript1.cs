using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScript1 : MonoBehaviour {
    public Animator anim;
    public float speed,flashDistance,reuseTime,force;
    public GameObject father;
    bool reuse = true, canHit = true;
    int times=0;
    public Transform groundCheck;
    private float groundedRadius= 0.2f;
    public LayerMask whatIsGround;
    public Rigidbody2D rb;
    public AudioSource swordHit,swordHit2;
    public AudioSource moveSound, jumpSound,flashSound;
    public int randomNum, randomTemp;
    public AudioClip[] jumpClips;
    [SerializeField]
    bool isGrounded;

    bool rightKeyDown, leftKeyDown, hitDown, flashDown, jumpDown;
    buttonsScript ButtonScriptRight, ButtonScriptLeft, ButtonScriptHit, ButtonScriptJump, ButtonScriptFlash;

    void Awake()
    {
        rb = father.GetComponent<Rigidbody2D>();
        ButtonScriptRight = GameObject.Find("rightButton").GetComponent<buttonsScript>();
        ButtonScriptLeft = GameObject.Find("leftButton").GetComponent<buttonsScript>();
        ButtonScriptHit = GameObject.Find("hitButton").GetComponent<buttonsScript>();
        ButtonScriptJump = GameObject.Find("jumpButton").GetComponent<buttonsScript>();
        ButtonScriptFlash = GameObject.Find("flashButton").GetComponent<buttonsScript>();

    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);
    }

	void Update () {
        anim.SetInteger("times", times);
        rightKeyDown = ButtonScriptRight.rightKeyDown;
        leftKeyDown = ButtonScriptLeft.leftKeyDown;
        hitDown = ButtonScriptHit.hitDown;
        flashDown = ButtonScriptFlash.flashDown;
        jumpDown = ButtonScriptJump.jumpDown;

        if ((Input.GetKey(KeyCode.RightArrow) || rightKeyDown)) //movement // can move while shield and jumped
        {
            moveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || leftKeyDown) //movement // can move while shield and jumped
        {
            moveLeft();
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow))|| (Input.GetKeyUp(KeyCode.LeftArrow))) //movement // can move while shield and jumped
        {
            anim.SetBool("running", false);
        }
        if (((Input.GetKeyDown(KeyCode.X)||flashDown))&&reuse) //movement // can move while shield and jumped
        {
            flash();
            reuse = false;
            StartCoroutine(reuseWait());
        }
        if ((Input.GetKeyUp(KeyCode.X))||!flashDown) //movement // can move while shield and jumped
        {
            unflash();
        }
        if (((Input.GetKeyDown(KeyCode.C)) || hitDown) &&canHit) // swordhit
        {
            swordHitVoid();
        }
        if ((Input.GetKeyDown(KeyCode.Space)&&isGrounded) || jumpDown) //jump 
        {
            jumpVoid();
        }

        if (!leftKeyDown && !rightKeyDown) //stop runing animation
        {
            anim.SetBool("running", false);
        }

    }
    public void moveRight()
    {
        father.transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("running", true);
    }
    public void moveLeft()
    {
        father.transform.Translate(Vector3.left * speed * Time.deltaTime);
        transform.localScale = new Vector3(-1, 1, 1);
       
            anim.SetBool("running", true);
    }
    public void flash()
    {
        flashSound.Play();
        anim.SetBool("flash", true);
        StartCoroutine(waitForAnimation());

    }
    public void unflash()
    {
        anim.SetBool("flash", false);
    }
    public void swordHitVoid()
    {
        anim.SetBool("attack", true);
        canHit = false;
        if (times >= 2)
        {
            times = 0;
        }
        times += 1;
        StartCoroutine(hitDelay());
        StartCoroutine(CanHitDelay());
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


    IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(0.400f); //wait for animation to finish
        Vector3 oldPos = father.transform.localPosition;
        Vector3 oldScale = transform.localScale;
        if(oldScale.x>0)
        father.transform.localPosition = new Vector3(oldPos.x + flashDistance, oldPos.y, oldPos.z);
        else
            father.transform.localPosition = new Vector3(oldPos.x - flashDistance, oldPos.y, oldPos.z);
    }
    IEnumerator reuseWait()
    {
        yield return new WaitForSeconds(reuseTime);
        reuse = true;
    }
    IEnumerator hitDelay()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("attack", false);
    }
    IEnumerator CanHitDelay()
    {
        yield return new WaitForSeconds(0.6f); //wait for animation to finish
        canHit = true;
    }
}
