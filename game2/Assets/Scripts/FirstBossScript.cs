using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossScript : MonoBehaviour {
    Vector2 knightVector, characterVector;
    float scale;
    int tempHealth;
    Animator anim;
    GameObject character,key;
    bool agro, swordhit1, death, canhit = true,bossHit=false;
    public float speed;
    public Image healthbar;
    public int health;


    // Use this for initialization
    void Start () {
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        character = GameObject.Find("Character1");
        key = GameObject.Find("key");
        //healthbar = GameObject.Find("fullHealthKnight").GetComponent<Image>();
        tempHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
        knightVector = this.gameObject.transform.position; //get position
        characterVector = character.gameObject.transform.position;//get position
        healthbar.fillAmount = ((float)tempHealth / (float)health);
        anim.SetFloat("distance", Mathf.Abs(characterVector.x - knightVector.x));

        if (knightVector.x - characterVector.x > 0) // stand up if character gets 5 units close 
        {
            if (knightVector.x - characterVector.x < 5)
            {
                agro = true;
                anim.SetBool("aggro", agro);
                //StartCoroutine(explosion());
            }
        }
        else
        {
            if (characterVector.x - knightVector.x < 5)
            {
                agro = true;
                anim.SetBool("aggro", agro);
                //StartCoroutine(explosion());
            }
        }
        if (knightVector.x - characterVector.x < 0)        // switch sides
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);
            if (characterVector.x - knightVector.x > 2f && agro && death == false) //keep some distance
            {
                if (!bossHit)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    anim.SetBool("hit", false);
                }
            }
            else
            {
                anim.SetBool("hit", true);
                bossHit = true;
                StartCoroutine(wait2());
            }
        }
        else
        {
                transform.localScale = new Vector3(2.5f, 2.5f, 1);
            if (knightVector.x - characterVector.x > 2f && agro && death == false) //keep some distance
            {
                if (!bossHit)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    anim.SetBool("hit", false);
                }
            }
            else
            {
                anim.SetBool("hit", true);
                bossHit = true;
                StartCoroutine(wait2());
            }
        }
        if (tempHealth <= 0) //kill knight
        {
            anim.SetBool("death", true);
            death = true;
            float x = transform.position.x;
            float y = transform.position.y;
            key.transform.position = new Vector2(x, y);
            StartCoroutine(wait());
        }

    }
    void OnTriggerEnter2D(Collider2D col )
    {
        if (col.gameObject.name == "hitCollider"  && canhit) 
        {
            tempHealth -= 1;
            canhit = false;
            StartCoroutine(wait1()); // 1 hit per animation

        }
        if (col.gameObject.name == "Skeleton" && col.GetType() == typeof(CircleCollider2D))
        {
            tempHealth -= 4;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.350f); //wait for animation to finish
        Destroy(this.gameObject);
    }
    IEnumerator wait1()
    {
        yield return new WaitForSeconds(0.220f); //wait for animation to finish
        canhit = true;
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1.6f); //wait for animation to finish
        bossHit = false;
    }


}

