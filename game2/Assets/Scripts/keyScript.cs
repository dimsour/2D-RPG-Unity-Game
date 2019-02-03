using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour {

    AudioSource keyAudio;
    void Awake()
    {
        keyAudio = GameObject.Find("keySound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            keyAudio.Play();
            Destroy(gameObject);
            GameObject.Find("GM").GetComponent<GM>().keys += 1;
        }
    }
}
