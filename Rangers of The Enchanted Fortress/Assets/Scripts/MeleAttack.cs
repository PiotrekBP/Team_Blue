using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject owner;
    void OnTriggerEnter(Collider target)
    {
        if(owner.name != target.name)
        {
            if(target.gameObject.layer!=10)
            {
                if(target.gameObject.layer==9 && target.gameObject==player)
                {
                    Debug.Log(target.name + "layer 9");//gracz
                }
                else if(target.gameObject.layer==11)
                {
                    LampCall lC = target.GetComponent<LampCall>();
                    Light lig = target.GetComponentInChildren<Light>();
                    lig.enabled = true;
                    lC.litUp = true;
                }
            }
        }
    }
}
