using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "shurikenLeft(Clone)" || col.gameObject.name == "shurikenRight(Clone)") //damage from shurikens
        {
            
            Destroy(col.gameObject);
            
        }
    }
}
