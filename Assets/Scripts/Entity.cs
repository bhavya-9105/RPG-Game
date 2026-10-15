using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    
    [Header("Attack Details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy; 


    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f; // forces unity to edit a privattte varable 
    // on the inspector window
    [SerializeField] private float jumpforce = 8;
    private bool facingRight = true;
    private float XInput;
    private bool canMove = true;
    private bool canJump = true;

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

    public void DamageEnenmies()
    {
        //public Collider2D[] enemyColliders; // set size, when you create and array //cannot add or remove items on the go // faster than list(performance)
        // public List<Collider2D> exampleList; // can have dynamic size. // can add or remove items on the go // slower than array(performance)
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);

        foreach (Collider2D enemy in enemyColliders)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage();

            string enemyName = enemyScript.GetEnemyName();
            Debug.Log("i damaged the enemy " + enemyName);
        }
    }

    public void EnableMovementAndJump( bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    private void HandleAnimator()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void HandleInput()
    {
        XInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();
    }

    private void TryToAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }

    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    }

    private void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity = new Vector2(XInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
