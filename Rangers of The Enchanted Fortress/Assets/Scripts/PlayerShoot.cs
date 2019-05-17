using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerShoot : MonoBehaviour
{
    public SoundManager SA;
    public AudioSource AS;
    public Collider platrig;
    private bool isAttacking = false;
    private float attackCooldown = 0f;
    private bool isStabbing = false;
    public Animator animator;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public ParticleSystem mainParticleSystem;
    public float Bullet_Forward_Force = 2f;
    public float AttackForward = 30f;
    private float fireTimer = 0;
    private bool willShoot = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        platrig.enabled = false;
    }

    void Update()
    {

        if (!Menu.isInteracting)
        {
            if (ShadowDetection.isInShadow)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    willShoot = true;
                    animator.SetTrigger("PShoot");


                }
            }
            if (ShadowDetection.isInShadow)
            {
                if (Input.GetMouseButton(0))
                {

                    isStabbing = true;

                }
            }

            if (isStabbing)
            {
                attackCooldown += Time.deltaTime;
                if (!isAttacking)
                {
                    AS.volume = 0.5f;
                    SA.PlaySound("stabbnocolision_sfx", AS);
                    animator.SetTrigger("Stab");
                    isAttacking = true;
                    platrig.enabled = true;
                    // StartCoroutine(WaitColl1(platrig));
                    StartCoroutine(WaitColl2(platrig));


                }
                else if (attackCooldown >= 1.5f)
                {
                    isAttacking = false;
                    attackCooldown = 0f;
                    isStabbing = false;

                }
            }
            if (willShoot)
            {
                fireTimer += Time.deltaTime;
            }
            if (fireTimer >= 0.45f)
            {
                AS.volume = 0.5f;
                SA.PlaySound("shoot3_sfx", AS);
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler, 0.4f);
                willShoot = false;
                fireTimer = 0;

            }
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if (isAttacking)
        {
            if (target.gameObject.layer == 10)
            {
                Debug.Log("Kill");
                Animator tani = target.GetComponent<Animator>();
                Light tlig = target.GetComponentInChildren<Light>();
                tlig.enabled = false;
                tani.SetTrigger("Dead");
                StartCoroutine(Wait(target.gameObject));


            }

        }
    }

    IEnumerator Wait(GameObject target)
    {
        AS.volume = 0.5f;
        SA.PlaySound("cma_death", AS);
        yield return new WaitForSeconds(1f);
        Destroy(target);
    }
    IEnumerator WaitColl2(Collider coll)
    {

        yield return new WaitForSeconds(0.65f);
        coll.enabled = false;
    }


}
