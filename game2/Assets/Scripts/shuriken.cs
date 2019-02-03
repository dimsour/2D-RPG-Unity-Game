using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour {
    Vector3 rotation;
    public float rotationSpeed,speed;
    public Transform child;
    public GameObject Ninja;


    void Update ()
    {
        child.transform.Rotate(Vector3.fwd * Time.deltaTime * rotationSpeed * 10);//spin shuriken sprite
        transform.Translate(Vector3.left * speed * Time.deltaTime);//move shuriken
        StartCoroutine(selfDestroy());  //auto destroy shuriken
    }

  /* void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name=="shield")
        {
            Destroy(gameObject);
            print("shield");
        }
        else if (col.gameObject.tag == "Player")
        {
            GameObject.Find("GM").GetComponent<GM>().health -= 1;
            Destroy(gameObject);
        }
        
    }*/
    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);

    }

}
