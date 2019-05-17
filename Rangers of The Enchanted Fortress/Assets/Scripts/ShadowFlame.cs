using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFlame : MonoBehaviour
{
    
    
    void OnTriggerEnter(Collider target)
    {
        if (this.name != target.name)
        {
            if ("Player" != target.gameObject.name && "hand.R_end" !=target.gameObject.name)
            {

                if (target.gameObject.layer == 11)
                {
                   
                    Transform parent = transform.parent;
                    LampCall lC = target.GetComponent<LampCall>();
                    Light lig = target.GetComponentInChildren<Light>();
                    lig.enabled = false;
                    lC.litUp = false;

                  

                    Destroy(parent.gameObject);
                }

                
            }
        }
    }
}
