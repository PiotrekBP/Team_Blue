using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float viewRange;
    public float viewAngle;
    public bool cansee;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float distanceToPlayer;
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position)<=viewRange&&!ShadowDetection.isInShadow)
        {
            if (Mathf.Abs(Vector3.Angle(transform.forward, (transform.position - player.position)))<=viewAngle)
            {
                Debug.Log("yesssss");
               // transform.LookAt(player);
                Vector3 direction = (transform.position - player.position);
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.08f);
            }
        }
        
    }
}
