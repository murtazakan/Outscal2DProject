﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb2d;


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    private void MoveCharacter(float horizontal) {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
    }

    private void PlayMovementAnimation(float horizontal) {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }


    private void FixedUpdate()
    {
        //walk animation
        float horizontal = Input.GetAxisRaw("Horizontal");

        MoveCharacter(horizontal);
        PlayMovementAnimation(horizontal);


        //crouch animation
        BoxCollider2D crouchCollider;
        crouchCollider = GetComponent<BoxCollider2D>();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
            crouchCollider.offset = new Vector3(-0.1084875f,0.5984814f);
            crouchCollider.size = new Vector3(0.9654864f,1.302302f);
        }
        else
        {
            animator.SetBool("Crouch", false);
            crouchCollider.offset = new Vector3(-0.01487844f, 0.9900814f);
            crouchCollider.size = new Vector3(0.4513782f, 2.020973f);
        }

        //jump 

        float jump = Input.GetAxisRaw("Vertical");

        if (jump > 0)
        {
            animator.SetBool("Jump", true);
            rb2d.velocity = Vector2.up * jumpForce;
        }
        else {
            animator.SetBool("Jump", false);
        }

    }
}
