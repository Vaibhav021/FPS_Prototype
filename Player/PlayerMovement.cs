using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("GameObjects")]
    public Camera normalCam;

    [Header("Movement Inputs")]
    public float moveSpeed = 400f;
    public float jumpForce = 5f;
    public float sprintModifier = 2f;

    [Header("Bools")]
    public bool isSprinting;
    public bool onGround;

    [Header("Ground Detection")]
    public Transform groundDetector;
    public LayerMask groundLayer;
       
    private float speed;

    private float baseFOV;
    private float sprintFOVModifier = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.enabled = false;
        rb = GetComponent<Rigidbody>();
        baseFOV = normalCam.fieldOfView;
        speed = moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        Movement();



    }

    public void Movement()
    {
        //--------------------Input--------------------

        float move_h = Input.GetAxis("Horizontal");
        float move_v = Input.GetAxis("Vertical");

        //--------------------Booling--------------------

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetButtonDown("Jump");
        onGround = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, groundLayer);

        //--------------------Movement--------------------

        Vector3 direction = new Vector3(move_h, 0f, move_v);
        //direction.Normalize();

        //--------------------Sprinting--------------------

        isSprinting = sprint && move_v > 0f;

        if(isSprinting)
        {
            speed = moveSpeed * sprintModifier;
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 10f);
        }
        else
        {
            speed = moveSpeed;
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 10f);
        }

        //--------------------Jumping--------------------

        if(jump && onGround)
        {
            rb.velocity = new Vector3(0f, jumpForce, 0f);
        }

        //--------------------Execution--------------------

        Vector3 t_velocity = transform.TransformDirection(direction) * speed * Time.deltaTime;
        t_velocity.y = rb.velocity.y;

        rb.velocity = t_velocity;


    }















}//class
