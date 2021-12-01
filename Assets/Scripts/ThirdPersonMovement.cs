using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float walkSpeed = 1.5f;
    public float sprintSpeed = 6f;
    float speed;

    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask grindRailMask;
    bool isGrounded;
    bool isGrinding;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrinding = Physics.CheckSphere(groundCheck.position, groundDistance, grindRailMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
            animator.SetBool("isJumping", false);
        }


        bool isSprinting = Input.GetKey("left shift");
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("isJumping", true);
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
