using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1 : MonoBehaviour {

    public Transform FollowPlatform;
    public Transform Player;

    Vector3 cameraValues;
    void Start()
    {
        cameraValues = FollowPlatform.position;
    }
    void Update()
    {
        FollowPlatform.position = new Vector3(Player.position.x, cameraValues.y,cameraValues.z);
    }
}



