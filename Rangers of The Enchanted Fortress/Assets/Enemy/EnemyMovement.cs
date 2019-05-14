using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform player;
    public static bool attacking;
    public static bool wasAttacking;
    public static bool wasSearching;
    private float searchTime = 0f;
    [SerializeField]
    private int enemyType;//0-mele 1-archer 2-mage
    private Vector3 nextSearch;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       // agent.autoBraking = false;
       // GotoNextPoint();
        
    }
 
    void GotoNextPoint()
    {
        if (points.Length != 0)
        {
            agent.SetDestination(points[destPoint].position);
            destPoint = (destPoint + 1) % points.Length;
        }
    }

    void Update()///////////zamienic na corutyne moze??
    {
        if (EnemyVision.canSee)
        {
            Debug.Log("attack");
            StartAttack();
            wasAttacking = true;
        }
        else if(!wasAttacking)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
        else
        {
            StartSearch();
        }
    }


    void StartAttack()
    {
        //if (attacking)
        //{
            if(enemyType==0)
            {
                if (Vector3.Distance(transform.position, player.position) >=2f)//mele
                {
                    agent.SetDestination(player.position);
                }
                else
                {
                    SwingWeapon();
                }
            }
            else if(enemyType==1)
            {
                if (Vector3.Distance(transform.position, player.position) >=7f)//archer
                {
                    agent.SetDestination(player.position);
                }
                else
                {
                    ShootArrow();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.position) >=5f)//mage
                {
                    agent.SetDestination(player.position);
                }
                else
                {
                    UseMagic();
                }
            }
            
        //}
        /*else
        {
            attacking = true;
        }*/
    }

   
    //enemyattacks
   
    void SwingWeapon()
    {
        //trigger
    }

    void ShootArrow()
    {
        //trigger
        //creating arrow
    }

    void UseMagic()
    {
        //trigger
        //magic???
    }

    //


    void StartSearch()
    {
        if (wasSearching)
        {
            searchTime += Time.deltaTime;
            if (searchTime >= 0 && searchTime < 2)
            {
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * 0.4f);
            }
            else if (searchTime >= 2 && searchTime < 4)
            {
                nextSearch = transform.position;
                nextSearch.x += 2f;
                agent.SetDestination(nextSearch);
            }
            else if (searchTime >= 4 && searchTime < 6)
            {
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * 0.4f);
            }
            else if (searchTime >= 6 && searchTime < 8)
            {
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * -0.4f);
            }
            else if (searchTime >= 8 && searchTime < 10)
            {
                nextSearch = transform.position;
                nextSearch.z += 2f;
                agent.SetDestination(nextSearch);
            }
            else
            {
                wasSearching = false;
                wasAttacking = false;
                searchTime = 0f;

            }
        }
        else
        {
                wasSearching = true;
                nextSearch = transform.position;
                nextSearch.x += 2f;
                agent.SetDestination(nextSearch);                        
        }

    }
}