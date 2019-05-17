using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public SoundManager SA;
    public AudioSource AS;
    public GameObject arrowEmiter;
    public GameObject arrow;
    public float arrowForce;

    public Transform lightToLit;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform player;
    public bool attacking;
    public bool wasAttacking =false;
    public bool wasSearching;
    private bool isAttacking;
    private float searchTime = 0f;
    [SerializeField]
    private int enemyType;//0-mele 1-archer 2-mage
    private Vector3 nextSearch;
    Animator animator;
    private float attackTimer = 0f;
    public bool lightCall = false;
    private bool willFire = false;
    private bool willFireMage = false;
    private float fireTimer = 0f;
    private float MagefireTimer = 0f;
    EnemyVision vis;

    // Start is called before the first frame update
    void Start()
    {
        vis = GetComponent<EnemyVision>();
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
        if (!Menu.isInteracting)
        {

            if (vis.canSee)
            {
                StartAttack();
                if(!wasAttacking)
                    SA.PlayCombatMusic();
                wasAttacking = true;
            }
            else if (lightCall)
            {
                if (Vector3.Distance(transform.position, lightToLit.position) >= 2f)
                {
                    agent.SetDestination(lightToLit.position);
                }
                else
                {
                    lightCall = false;
                    SwingWeapon();
                    isAttacking = false;
                }
            }
            else if (!wasAttacking)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    agent.isStopped = false;
                    animator.SetBool("Walk", true);
                    GotoNextPoint();
                }
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("Walk", true);
                SA.PlaySearchMusic();
                StartSearch();
            }
            if (willFire)
            {
                fireTimer += Time.deltaTime;
            }
            if (fireTimer >= 0.45f)
            {
                GameObject tempArrow;
                tempArrow = Instantiate(arrow, arrowEmiter.transform.position, arrowEmiter.transform.rotation);
                //tempArrow.transform.Rotate(Vector3.left * 90);
                Rigidbody tempArrowRigidbody;
                tempArrowRigidbody = tempArrow.GetComponent<Rigidbody>();
                tempArrowRigidbody.AddForce(transform.forward * arrowForce);
                Destroy(tempArrow, 3f);
                fireTimer = 0;
                willFire = false;

            }
            if (willFireMage)
            {
                MagefireTimer += Time.deltaTime;
            }
            if (MagefireTimer >= 0.40f)
            {
                GameObject tempArrow;
                tempArrow = Instantiate(arrow, arrowEmiter.transform.position, arrowEmiter.transform.rotation);
                //tempArrow.transform.Rotate(Vector3.left * 90);
                Rigidbody tempArrowRigidbody;
                tempArrowRigidbody = tempArrow.GetComponent<Rigidbody>();
                tempArrowRigidbody.AddForce(transform.forward * arrowForce);
                Destroy(tempArrow, 0.5f);
                MagefireTimer = 0;
                willFireMage = false;

            }
        }
    }


    void StartAttack()
    {
        
        if (enemyType == 0)
        {
            if (Vector3.Distance(transform.position, player.position) >= 2.1f)//mele
            {
                animator.SetBool("Walk", true);
                //Debug.Log("attacking");
                agent.SetDestination(player.position);
            }
            else
            {

                LookAtTarget();
                agent.isStopped = true;
                animator.SetBool("Walk", false);
                SwingWeapon();
            }
        }
        else if (enemyType == 1)
        {
            Debug.Log("Arch");
            if (Vector3.Distance(transform.position, player.position) >= 10f)//archer
            {
                agent.SetDestination(player.position);
                animator.SetBool("Walk", true);
            }
            else
            {
                LookAtTarget();
                agent.isStopped = true;
                animator.SetBool("Walk", false);
                ShootArrow();
                
            }
        }
        else if(enemyType ==2)
        {
            if (Vector3.Distance(transform.position, player.position) >= 6f)//mage
            {
                agent.SetDestination(player.position);
                animator.SetBool("Walk", true);
            }
            else
            {
                LookAtTarget();
                agent.isStopped = true;
                animator.SetBool("Walk", false);
                ShootFire();
            }
        }


        
    }


    //enemyattacks

    void SwingWeapon()
    {
        attackTimer += Time.deltaTime;
        if (!isAttacking)
        {
            SA.PlaySound("maczuga", AS);
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
            SA.PlaySound("bow_sfx", AS);
            isAttacking = true;
            animator.SetTrigger("Shoot");
            willFire = true;

        }
        else if (attackTimer >= 3f)
        {
            isAttacking = false;
            attackTimer = 0f;
        }
    }
    void ShootFire()
    {
        attackTimer += Time.deltaTime;
        if (!isAttacking)
        {
            SA.PlaySound("strzalognia_maga", AS);
            isAttacking = true;
            animator.SetTrigger("Shoot");
            willFireMage = true;

        }
        else if (attackTimer >= 3f)
        {
            isAttacking = false;
            attackTimer = 0f;
        }


    }


    //


    void StartSearch()
    {
        if (wasSearching)
        {
            searchTime += Time.deltaTime;
            if (searchTime >= 0 && searchTime < 2)
            {
                agent.isStopped = true;
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * 0.4f);
                animator.SetBool("Walk", false);
            }
            else if (searchTime >= 2 && searchTime < 4)
            {
                agent.isStopped = false;
                nextSearch = transform.position;
                nextSearch.x += 2f;
                agent.SetDestination(nextSearch);
                animator.SetBool("Walk", true);
            }
            else if (searchTime >= 4 && searchTime < 6)
            {
                agent.isStopped = true;
                animator.SetBool("Walk", false);
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * 0.4f);
            }
            else if (searchTime >= 6 && searchTime < 8)
            {
                animator.SetBool("Walk", false);
                transform.Rotate(Vector3.up, (searchTime - 4) / 2 * -0.4f);
            }
            else if (searchTime >= 8 && searchTime < 10)
            {
                agent.isStopped = false;
                animator.SetBool("Walk", true);
                nextSearch = transform.position;
                nextSearch.z += 2f;
                agent.SetDestination(nextSearch);
            }
            else
            {
                wasSearching = false;
                wasAttacking = false;
                searchTime = 0f;
                SA.PlayCalmMusic();

            }
        }
        else
        {
            animator.SetBool("Walk", true);
            wasSearching = true;
            nextSearch = transform.position;
            nextSearch.x += 2f;
            agent.SetDestination(nextSearch);
        }
        

    }
    void LookAtTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
   


}