using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    
    void OnTriggerEnter(Collider target)
    {
        if (this.name != target.name)
        {
            if ("Player" == target.gameObject.name)
            {                   
                    Transform parent = transform.parent;
                    
                    Destroy(parent.gameObject);
                    ShadowDetection.Die = true;
                                    
            }
        }
    }
    
}
