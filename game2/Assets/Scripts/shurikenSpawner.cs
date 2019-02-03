using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenSpawner : MonoBehaviour {

    GameObject shuriken;
    public float delay;
    public bool stop;
    Vector3 spawnPosition;
    public shuriken shurikenScript;

    void Update()
    {
        if(transform.parent.localScale.x > 0 ) // right or left shuriken prefab
            shuriken = (GameObject)Resources.Load("prefabs/shurikenLeft", typeof(GameObject));
        else
            shuriken = (GameObject)Resources.Load("prefabs/shurikenRight", typeof(GameObject));

        spawnPosition = gameObject.transform.position;
        if (!stop)
        StartCoroutine(waitSpawner());
    }

    IEnumerator waitSpawner()
    {
        stop = true;
        yield return new WaitForSeconds(delay);
        Instantiate((shuriken),spawnPosition,gameObject.transform.rotation);
        stop = false;

    }
}
