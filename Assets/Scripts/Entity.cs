using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration =0.2f ;
    private Coroutine damageFeedbackCoroutine;
    
    
    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatisTarget; 


    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f; // forces unity to edit a privattte varable 
    // on the inspector window
    [SerializeField] private float jumpforce = 8;
    protected int facingDir = 1;
    protected bool canMove = true;
    private bool facingRight = true;
    private bool canJump = true;
    private float XInput;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimator();
        HandleFlip();
    }

    public void DamageTargets()
    {
        //public Collider2D[] enemyColliders; // set size, when you create and array //cannot add or remove items on the go // faster than list(performance)
        // public List<Collider2D> exampleList; // can have dynamic size. // can add or remove items on the go // slower than array(performance)
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatisTarget);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;

        PlayDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
            StopCoroutine(damageFeedbackCoroutine);

        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMat = sr.material;

        sr.material = damageMaterial;

        yield return new WaitForSeconds(damageFeedbackDuration);

        sr.material = originalMat;
    }

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);
    }

    public void EnableMovementAndJump( bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    protected void HandleAnimator()
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
            HandleAttack();
    }

    protected virtual void HandleAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }

    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
    }

    protected virtual void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity = new Vector2(XInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }



    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    protected void HandleFlip()
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
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
