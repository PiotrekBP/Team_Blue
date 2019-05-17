using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCall : MonoBehaviour
{
    public List<GameObject> Enemies;//only type 0!!!
    public bool litUp {
        set {
            if (value != _litUp)
            { 
                _litUp = value;
                OnLitChange();
            }
        }
        get { return _litUp; } }
    bool _litUp=true;
    
    
    void OnLitChange()
    {
        if (litUp == false)
        {
            if (Enemies.Count != 0)
            {
                GameObject closest = Enemies[0];
                foreach (GameObject x in Enemies)
                {
                    if (Vector3.Distance(transform.position, x.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                    {
                        closest = x;
                    }
                }
                EnemyMovement script = closest.GetComponent<EnemyMovement>();
                script.lightToLit = transform;
                script.lightCall = true;
            }
        }
    }  
    
}
