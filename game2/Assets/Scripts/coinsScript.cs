using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsScript : MonoBehaviour {

    int coins;
    public coinsScript coinsScript1;
    public AudioSource coinSound;
    void  Awake()
    {
         //coins=GameObject.Find("GM").GetComponent<GM>().coins;
         coinSound = GameObject.Find("coinSound").GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            coinSound.Play();
            Destroy(gameObject);
            //GameObject.Find("GM").GetComponent<GM>().coins += 1;
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1);
        }
    }
}
