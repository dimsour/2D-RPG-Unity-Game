using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageController : MonoBehaviour {

    public AudioSource damageAudio;
    public AudioClip[] damageSounds;
    int randomNum,randomTemp;
    public int health=3;
    public bool blocked;
    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "shurikenLeft(Clone)" || col.gameObject.name == "shurikenRight(Clone)") //damage from shurikens
        {
            damageSound();
            Destroy(col.gameObject);
            health--;
        }
        if(col.gameObject.name=="Skeleton"&&col.GetType()==typeof(CircleCollider2D)) //damage from skeleton
        {
            damageSound();
            //damage in skeletonScript
        }
        if(col.gameObject.tag=="spikes")//damage from spikes
        {
            damageSound();
            health -= 3;
        }
        if (col.gameObject.name == "BlackKnight" && col.GetType() == typeof(PolygonCollider2D))
        {
           
                damageSound();
                health -= 1;
            
        }
        if (col.gameObject.name == "1boss" && col.GetType() == typeof(PolygonCollider2D))
        {
            
                damageSound();
                health -= 1;
            
        }
    }
    public void damageSound() 
    {
        randomNum = Random.RandomRange(0, 3);
        while (randomNum == randomTemp) // so it wont play the same clip twice
        {
            randomNum = Random.RandomRange(0, 3);
        }
        randomTemp = randomNum;
        damageAudio.clip = damageSounds[randomNum];
        damageAudio.Play();
    }
}
