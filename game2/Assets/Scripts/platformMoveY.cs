using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMoveY : MonoBehaviour
{
    Vector3 startingY, downY, upY;
    public bool down, up;//set witch way it will start from editor
    public float speed, downValue, upValue;
    public Transform childTransform;
    void Start()
    {
        startingY = transform.position; //get starting position
        downY = new Vector2(startingY.x , startingY.y - downValue); // set the range
        upY = new Vector2(startingY.x , startingY.y + upValue); // set the range
    }

    void Update()
    {
        if (upValue != 0 || downValue != 0)
        {
            if (down) //move platformer 
                transform.Translate(Vector2.down * Time.deltaTime * speed);
            else
                transform.Translate(Vector2.up * Time.deltaTime * speed);

            if (transform.position.y <= downY.y) //change direction
            {
                down = false;
                up = true;
            }
            if (transform.position.y >= upY.y)//change direction
            {
                up = false;
                down = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) //make player child so it moves with platformer
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(childTransform);
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
