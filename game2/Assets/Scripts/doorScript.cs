using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorScript : MonoBehaviour {

    int totalKeys,keys;
    string obj;
    public string nextScene;
    Vector2 doorVector;
    Vector2 characterVector;
    GameObject character;
    public bool inRange;
    public AudioSource doorSound;
    public Text doorText;
    public Animator anim;
    void Start ()
    {
        totalKeys = GameObject.FindGameObjectsWithTag("keys").Length;
        character = GameObject.Find("Character1");
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        doorVector = this.gameObject.transform.position; //get position
        characterVector = character.gameObject.transform.position;//get position
        keys = GameObject.Find("GM").GetComponent<GM>().keys;

        if ((totalKeys == keys))
        {
            if (Input.GetMouseButtonDown(0))
            {
                CastRay();
                if (obj == "door")
                {
                    if (inRange)
                    {
                        doorSound.Play();
                        anim.SetBool("doorOpen", true);
                        StartCoroutine( loadLevel());
                    }
                   
                }
            }
        }
        if ((Mathf.Abs(doorVector.x - characterVector.x) < 2)&&(Mathf.Abs(doorVector.y-characterVector.y)<2))
            inRange = true;
        else
            inRange = false;

        if(totalKeys==keys)
            doorText.text = "You can enter!";
        else
             doorText.text = (keys + "/" + totalKeys);
        
    }
    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            Debug.Log(hit.collider.gameObject.name);
            obj= (hit.collider.gameObject.name).ToString();
        }
    }
    IEnumerator loadLevel()
    {
       yield return new WaitForSeconds(1);
        Application.LoadLevel(nextScene);
    }
    }
