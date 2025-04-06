using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Animator animator;
 
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
 
    Vector3 velocity;
 
    bool isGrounded;
    float sprintMultiplier = 1;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        sprintMultiplier = 1;
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            sprintMultiplier = 2;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && isGrounded) {
            sprintMultiplier = 1;
        }
        

        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 0) {
            animator.SetBool("isRunning", true);
  
        }

        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);

        }



        controller.Move(move * speed * sprintMultiplier * Time.deltaTime);
 
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
}