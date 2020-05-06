using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float walkSpeed = 3;
    public float sprintSpeed = 6;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    public float jumpSpeed = 0.2f;
    public float hyperJump = 2;
    public AudioSource audioS;
    float powerJump;
    public float gravity = 1.0f;
    static float t = 0.0f;


    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;


    private float verticalVel;
    private Vector3 moveVector;

    // Use this for initialization
    void Start()
    {
        Resources.Load("Sounds");
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        isGrounded = controller.isGrounded;
		if (isGrounded) {
            moveVector.y = 0.001f;
            anim.SetBool("falling", false);

            if (Input.GetKey("space"))
            {
                anim.SetBool("charging",true); //CHARGE THAT JUMP
                powerJump = Mathf.Lerp(jumpSpeed, hyperJump, t); //POWER THAT JUMP
                t += 0.25f * Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("charging", false); //STOP CHARGING WE DONE HERE
                anim.SetTrigger("jump"); //SUPER JUMP
                print(powerJump);
                verticalVel = powerJump;
                powerJump = jumpSpeed;
                t = 0.0f;

            }
        }
        else
        {
            verticalVel -= gravity * Time.deltaTime;
            anim.SetBool("falling", true);
        }

        moveVector.y = verticalVel;
        moveVector = new Vector3 (0, verticalVel, 0);
        controller.Move(moveVector);
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;
        

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;
        if (Input.GetKey(KeyCode.LeftShift))
            Speed = sprintSpeed;
        else
            Speed = walkSpeed;

        desiredMoveDirection *= Speed;

        if (GetComponent<ThrowController>().aiming)
            return;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * 3);
        }
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat ("InputMagnitude", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
            audioS.enabled = true;
            audioS.loop = true;

        }
        else if (Speed < allowPlayerRotation)
        {
            audioS.enabled = false;
            audioS.loop = false;
            anim.SetFloat ("InputMagnitude", Speed, StopAnimTime, Time.deltaTime);
        }
    }
}
