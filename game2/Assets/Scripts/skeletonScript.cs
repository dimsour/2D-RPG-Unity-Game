using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonScript : MonoBehaviour
{

    GameObject character;
    Vector2 skeletonVector;
    Vector2 characterVector;
    public int speed,damage;
    float scale;
    public Animator anim;
    bool swordhit1 = true, agro, boom;
    public GameObject skeletonObj, explosionObj,healthBar;
    public float time;

    void Awake()
    {
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        character = GameObject.Find("Character1");

    }
    void Update()
    {

        skeletonVector = this.gameObject.transform.position; //get position
        characterVector = character.gameObject.transform.position;//get position

        if (skeletonVector.x - characterVector.x > 0) // stand up if character gets 5 units close 
        {
            if (skeletonVector.x - characterVector.x < 5)
            {
                agro = true;
                anim.SetBool("aggro", agro);
                StartCoroutine(explosion());
            }
        }
        else
        {
            if (characterVector.x - skeletonVector.x < 5)
            {
                agro = true;
                anim.SetBool("aggro", agro);
                StartCoroutine(explosion());
            }
        }
        if (skeletonVector.x - characterVector.x < 0)        // switch sides
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("skeletonWalk") && agro && !boom) // if stand animation is finished start chasing
            {
                if (characterVector.x - skeletonVector.x > 0.5f) //keep some distance
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("skeletonWalk") && agro && !boom)// if stand animation is finished start chasing
            {
                if (skeletonVector.x - characterVector.x > 0.5f) //keep some distance
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(boom) //take damage
        {
            if (col.gameObject.tag=="Player")
            {
                GameObject.Find("damageControll").GetComponent<damageController>().health -= damage;
            }
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.230f); //wait for animation to finish
        swordhit1 = true;
    }
    IEnumerator explosion()
    {
        yield return new WaitForSeconds(time); //EXPLOSION
        boom = true;
        skeletonObj.SetActive(false);
        explosionObj.SetActive(true);
        anim.SetBool("explosion", true);
        yield return new WaitForSeconds(0.76f);//wait explosion animation
        Destroy(this.gameObject);
    }

}
