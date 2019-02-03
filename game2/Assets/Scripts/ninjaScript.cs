using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaScript : MonoBehaviour {

    public int health=2;
    GameObject character;
    Vector2 characterVector;
    Vector2 ninjaVector;
    public float scale;
    bool swordhit1=true;

    void Awake()
    {
        scale = transform.localScale.x;
        character = GameObject.Find("Character1");
    }
    void Update()
    {
        ninjaVector = this.gameObject.transform.position; //get position
        characterVector = character.gameObject.transform.position;//get position

        if (ninjaVector.x - characterVector.x <0)        // switch sides
            transform.localScale = new Vector3(-1,1,1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (health<=0) //kill ninja
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name=="hitCollider" )
        {
            health -= 1;
            print("hit");
            swordhit1 = false;
            StartCoroutine(wait()); // 1 health for each animation
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f); //wait for animation to finish
        swordhit1 = true;
    }
}
