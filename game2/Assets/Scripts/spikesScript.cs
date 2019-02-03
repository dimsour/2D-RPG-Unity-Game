using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{
    public float maxspeed,upValue;
    float speed;
    float maxVector,startVector;
    public bool top=false;
    public AudioSource spikeSound;
    public bool isVisible;

    void  Start()
    {
        maxVector = transform.localPosition.y + upValue;
        startVector = transform.localPosition.y-1;
    }
    void Update()
    {
        if (transform.localPosition.y <= maxVector&&!top) //move spikes up
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * 2);
            if (Mathf.Round(transform.localPosition.y) == Mathf.Round(maxVector))
            {
                top = true;
                
            }
        }
        if(transform.localPosition.y >=startVector&& top) //move spikes down
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed / 5);
            if (Mathf.Round(transform.localPosition.y) == Mathf.Round(startVector))
            {
                top = false;
                if(GetComponent<Renderer>().isVisible) // play sound only if spikes are visible
                StartCoroutine(delay());
                
            }
        }
        if(speed<=maxspeed)
        {
            speed +=0.5f; //because it doesnt work if i set it at full speed // dunno why
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        spikeSound.Play();
    }

}
