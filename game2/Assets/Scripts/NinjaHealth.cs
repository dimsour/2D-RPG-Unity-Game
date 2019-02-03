using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHealth : MonoBehaviour {

    public Transform healthHalf, HealthFull;
    public int health;
    public void Update()
    {
        if (transform.parent.localScale.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

    
        health = gameObject.GetComponentInParent<ninjaScript>().health;
        if(health==4)
        {
            HealthFull.gameObject.SetActive(true);
            healthHalf.gameObject.SetActive(false);
        }
        else if(health==2)
        {
            HealthFull.gameObject.SetActive(false);
            healthHalf.gameObject.SetActive(true);
        }
    }
}
