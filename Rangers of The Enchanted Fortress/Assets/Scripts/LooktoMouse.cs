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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Vector3 target = hit.point;
            target.y = 0;
            target.y = transform.localScale.y / 2f;

            transform.LookAt(target);
        }
    }
}
