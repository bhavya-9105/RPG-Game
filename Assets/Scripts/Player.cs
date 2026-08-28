using System;
using UnityEngine;

public class Script : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f; // forces unity to edit a privattte varable 
    // on the inspector window
    [SerializeField] private float jumpforce = 8;
    private bool facingRight = true;
    private float XInput;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimator();
        HandleFlip();
    }
    private void HandleAnimator()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("IsMoving", isMoving);
    }

    private void HandleInput()
    {
        XInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        Jump();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(XInput * moveSpeed ,rb.linearVelocity.y);
    }

    private void Jump()
    {
        if(isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if(rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
