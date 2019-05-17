using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SoundManager SA;
    public AudioSource AS;
    CharacterController characterController;
    public Animator animator;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 inputVector = Vector3.zero;
    private Vector3 temp;
    private Vector3 moveDirection;
    private bool playingm = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (!Menu.isInteracting)
        {
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes

                inputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = Quaternion.AngleAxis(-45, Vector3.up) * inputVector;
                moveDirection *= speed;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
                {
                    animator.SetBool("isWalk", true);
                    AS.volume = 0.5f;
                    if(!playingm)
                    {
                        AS.loop = true;
                        SA.PlaySound("player_move_loop", AS);
                        playingm = true;
                    }
                    
                }
                else
                {
                    AS.loop = false;
                    animator.SetBool("isWalk", false);
                    playingm = false;
                    SA.StopSound(AS);
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}

//private Rigidbody player;
//public float movementSpeed;
//// Start is called before the first frame update
//void Start()
//{
//    player = GetComponent<Rigidbody>();
//    movementSpeed = 10f;
//}

//// Update is called once per frame
//void Update()
//{
//    float hor = Input.GetAxisRaw("Horizontal");
//    float ver = Input.GetAxisRaw("Vertical");
//    Vector3 movement = new Vector3(hor,0f, ver);
//    player.MovePosition(transform.position + (movement.normalized * movementSpeed) * Time.deltaTime);



//    //transform.Translate(movementSpeed*Input.GetAxis("Horizontal") * Time.deltaTime, 0f, movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

//}

