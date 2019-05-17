using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooktoMouse : MonoBehaviour
{
    Camera cam;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Menu.isInteracting)
        {
            float speed = 4.0f;


            var playerPlane = new Plane(Vector3.up, transform.position);
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {

                var targetPoint = ray.GetPoint(hitdist);
                var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }
        }
    }
}
