using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /* public Transform target;
     public float speed = 0.125f;
     //0to1
     public Vector3 offset;

     void LateUpdate()
     {
         offset = new Vector3(0, 10f, -10f);
         Vector3 desiredPosition = target.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
         transform.position = smoothedPosition;

         transform.LookAt(target);


     }*/

    public Transform target;
    public float speed = 0f;
    //0to1
    public Vector3 offset = new Vector3(0, 10f, -10f);

    void LateUpdate()
    {

        Vector3 desiredPosition = target.position + offset;

        //smooth
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;

    }
}
