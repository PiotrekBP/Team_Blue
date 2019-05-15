using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject arrowEmiter;
    public GameObject arrow;
    public float arrowForce;

    public Transform lightToLit;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform player;
    public static bool attacking;
    public static bool wasAttacking;
    public static bool wasSearching;
    private bool isAttacking;
    private float searchTime = 0f;
    [SerializeField]
    private int enemyType;//0-mele 1-archer 2-mage
    private Vector3 nextSearch;
    Animator animator;
    private float attackTimer = 0f;
    public bool lightCall = false;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();//getcomponent not in child it is only for test????
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
        else if (!wasAttacking)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
        else if(lightCall)
        {
            if(Vector3.Distance(transform.position, lightToLit.position) >= 2f)
            {
                agent.SetDestination(lightToLit.position);
            }
            else
            {
                lightCall = false;
                SwingWeapon();
            }
        }
        else
        {
            StartSearch();
        }
    }


    void StartAttack()
    {
       
        if (enemyType == 0)
        {
            if (Vector3.Distance(transform.position, player.position) >= 2f)//mele
            {
                Debug.Log("attacking");
                //Debug.Log(Vector3.Distance(transform.position, player.position));
                agent.SetDestination(player.position);
            }
            else
            {
                SwingWeapon();
            }
        }
        else if (enemyType == 1)
        {
            if (Vector3.Distance(transform.position, player.position) >= 7f)//archer
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
            if (Vector3.Distance(transform.position, player.position) >= 5f)//mage
            {
                agent.SetDestination(player.position);
            }
            else
            {
                UseMagic();
            }
        }

        
    }


    //enemyattacks

    void SwingWeapon()
    {
        attackTimer += Time.deltaTime;
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Swing");
        }
        else if (attackTimer >= 3f)
        {
            isAttacking = false;
            attackTimer = 0f;
        }
    }

    void ShootArrow()
    {
        attackTimer += Time.deltaTime;
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Shoot");
            CreateArrow();

        }
        else if (attackTimer >= 3f)
        {
            isAttacking = false;
            attackTimer = 0f;
        }
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

    void OnTriggerEnter(Collider target)//dodac pierwszenstow strzal
    {
        if (isAttacking)
        {
            if (target.gameObject.layer == 9)
            {
                if (!target.GetComponent<CharacterController>())
                {
                    Debug.Log("Is not a player.");
                    Debug.Log(target.name);
                }
                else
                    Debug.Log("GameOver");
            }
            else if(target.gameObject.layer == 11)
            {
                //turnonlamp
            }
        }

    }

    void CreateArrow()
    {
        GameObject tempArrow;
        tempArrow = Instantiate(arrow, arrowEmiter.transform.position, arrowEmiter.transform.rotation);
        Rigidbody tempArrowRigidbody;
        tempArrowRigidbody = tempArrow.GetComponent<Rigidbody>();
        tempArrowRigidbody.AddForce(transform.forward * arrowForce);
        Destroy(tempArrow, 10f);
    }

}