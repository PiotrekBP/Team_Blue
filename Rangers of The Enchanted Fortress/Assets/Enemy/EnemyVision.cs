using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float viewRange;
    public float viewAngle;
    public static bool canSee;
    private float distanceToPlayer;
    void Start()
    {
        canSee = false;
    }
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position)<=viewRange&&!ShadowDetection.isInShadow)
        {
            if (Mathf.Abs(Vector3.Angle(transform.forward, (transform.position - player.position)))<=viewAngle)
            {
                canSee = true;
                ////////////////niepotrzebne tylko do testow.
                Debug.Log("yesssss");
              //  Vector3 direction = (transform.position - player.position);
            //    Quaternion lookRotation = Quaternion.LookRotation(direction);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.08f);
                /////////////////////////////////
            }
            else
            {
                canSee = false;
            }
        }
        else
        {
            canSee = false;
        }
        
    }
}
