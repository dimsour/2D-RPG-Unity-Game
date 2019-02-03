using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalScript : MonoBehaviour {

    public AudioSource crystalAudio;

    void Awake()
    {
        crystalAudio = GameObject.Find("crystalSound").GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            crystalAudio.Play();
            Destroy(gameObject);
            GameObject.Find("GM").GetComponent<GM>().crystals += 1;
        }
    }
}
