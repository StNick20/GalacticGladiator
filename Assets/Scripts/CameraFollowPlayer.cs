using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform followTransform;
    private bool followPlayer = true;
    
    void FixedUpdate()
    {
        if (followPlayer) {

        if (followTransform != null){
     this.transform.position =  new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z); 
        }
        else {
            followPlayer = false;
        }
        }
    }
}
