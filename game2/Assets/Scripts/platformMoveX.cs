using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMoveX : MonoBehaviour {

    Vector3 startingX,leftX,rightX;
    public bool left, right;//set witch way it will start from editor
    public float speed,leftValue,rightValue;
    public Transform childTransform;
	void Start ()
    {
         startingX= transform.position; //get starting position
        leftX = new Vector2(startingX.x - leftValue, startingX.y); // set the range
        rightX = new Vector2(startingX.x + rightValue, startingX.y); // set the range
	}

    void Update()
    {
        if (rightValue != 0 || leftValue != 0)
        {
            if (left) //move platformer 
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            else
                transform.Translate(Vector2.right * Time.deltaTime * speed);

            if (transform.position.x <= leftX.x) //change direction
            {
                left = false;
                right = true;
            }
            if (transform.position.x >= rightX.x)//change direction
            {
                right = false;
                left = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) //make player child so it moves with platformer
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(childTransform);
            print("collOK");
        }
    }
    void OnCollisionExit2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(null);
        }
    }
}

