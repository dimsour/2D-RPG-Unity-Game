using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsScript : MonoBehaviour
{

   public bool rightKeyDown, leftKeyDown,hitDown,flashDown,jumpDown;

    public void down()
    {
        if (this.gameObject.name == ("rightButton"))
            rightKeyDown = true;
        else if(this.gameObject.name==("leftButton"))
            leftKeyDown = true;
        else if (this.gameObject.name == ("hitButton"))
            hitDown = true;
        else if (this.gameObject.name == ("flashButton"))
            flashDown = true;

        
    }
    public void up()
    {
        if (this.gameObject.name == ("rightButton"))
            rightKeyDown = false;
        else if (this.gameObject.name == ("leftButton"))
            leftKeyDown = false;
        else if (this.gameObject.name == ("hitButton"))
            hitDown = false;
        //else if (this.gameObject.name == ("shieldButton"))
            //shieldDown = false;
        else if (this.gameObject.name == ("flashButton"))
            flashDown = false;
    }
    public void jump()
    {
        jumpDown = true;
    }

}
