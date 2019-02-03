using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldScript : MonoBehaviour {
    public AudioSource shieldSound;
    public float shieldDurability,counter,recovery;
    public bool blocked=false;
    void Start()
    {
        counter = shieldDurability;
    }
    void Update()
    {
        if(counter<=0)
        {
            GameObject.Find("damageControll").GetComponent<damageController>().health -= 1;
            GameObject.Find("damageControll").GetComponent<damageController>().damageSound();
            counter = shieldDurability;
        }
        counter += Time.deltaTime * recovery/100;
        if(counter>shieldDurability)
        {
            counter = shieldDurability;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "shurikenLeft(Clone)" || col.gameObject.name == "shurikenRight(Clone)")
        {
            Destroy(col.gameObject);
            shieldSound.Play();
            counter--;
        }
        if (col.gameObject.name == "BlackKnight" && col.GetType() == typeof(PolygonCollider2D))
        {
            print("test");

            blocked = true;
            shieldSound.Play();
            counter--;
            StartCoroutine(unBlock());
        }
        if (col.gameObject.name == "1boss" && col.GetType() == typeof(PolygonCollider2D))
        {
            blocked = true;
            shieldSound.Play();
            counter--;
            StartCoroutine(unBlock());
        }
    }
    IEnumerator unBlock()
    {
        yield return new WaitForSeconds(0.15f);
            blocked = false;
    }
}
