using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Horizontal Movement")]
    public float playerAcceleration = 25;
    public float playerDeceleration = 30;
    public float xMaxSpeed = 10; 
    private float playerDirection;
    private bool facingRight;
    private float xSpeed;

    [Header("Vertical Movement")]
    public float jumpForce;
    public float longJumpMult = 1;
    public float yMaxSpeed = 20;
    private float gravity;
    private float ySpeed;

    [Header("Dash Settings")]
    public bool dash;
    public float dashForce = 50;
    public float dashTime;
    private float dashTimeCount;
    private bool isDashing;
    private bool dashInput;
    private bool youCanDash;
    private float cooldown;
    public float cooldownTime;  

    [Header("Collision Settings")]
    public bool isGrounded;
    public float groundRadius = 0.2f;
    public Transform groundCheckPoint;
    public LayerMask groundLayerMask;

    [Header("Inputs")]
    private float horizontalInput;
    private bool jumpButton;
    private bool longJumpButton;

    [Header("Animation Settings")]
    public Animator animator;

    private void Start()
    {
        playerDirection = 1;
        facingRight = true;

        gravity = Physics2D.gravity.y;
    }

    private void Update()
    {
        GetInputs();

        GroundCheck();
        Movement();

        SetAnimatorParameters();
    }
    private void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        dashInput = Input.GetKeyDown(KeyCode.L);

        cooldown += Time.deltaTime;

        if (cooldown > cooldownTime)
        {
            youCanDash = true;
            cooldown = 0;
        }

        if (dashInput && youCanDash)
        {
            isDashing = true;
        }

        jumpButton = Input.GetButtonDown("Jump");
        longJumpButton = Input.GetButton("Jump");
    }

    private void SetAnimatorParameters()
    {
        animator.SetFloat("horizontalSpeed", Mathf.Abs(xSpeed));
        animator.SetFloat("verticalSpeed", ySpeed);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDashing", isDashing);
    }
    private void Movement()
    {
        HorizontalMovement();
        VerticalMovement();

        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    private void HorizontalMovement()
    {
        cooldown += Time.deltaTime;

        if (cooldown > cooldownTime)
        {
            youCanDash = true;
            cooldown = 0;
        }

        if (isDashing)
        {
            Dash();

            return;
        }
        
        if (Mathf.Abs(horizontalInput) > 0)
        {
            Direction();

            xSpeed += horizontalInput * playerAcceleration * Time.deltaTime;
            xSpeed = Mathf.Clamp(xSpeed, -xMaxSpeed, xMaxSpeed);
        }

        else
        {
            xSpeed = Mathf.MoveTowards(xSpeed, 0, playerDeceleration * Time.deltaTime);
        }
    }
    private void Direction()
    {
        playerDirection = horizontalInput;

        if (facingRight && playerDirection < 0)
        {
            Flip();
        }
        if (!facingRight && playerDirection > 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        transform.localScale = new Vector3(playerScale.x * -1, playerScale.y, playerScale.z);

    }

    private void Dash()
    {
        dashTimeCount += Time.deltaTime;

        xSpeed = dashForce * playerDirection;

        if(dashTimeCount > dashTime)
        {
            xSpeed = xMaxSpeed * playerDirection;
            dashTimeCount = 0;
            isDashing = false;
            youCanDash = false;
        }
    }
    private void VerticalMovement()
    {
        if (isGrounded && jumpButton)
        {
            ySpeed = jumpForce;
            animator.SetTrigger("jump");
        }
        else if (longJumpButton && ySpeed > 0 && !isGrounded)
        {
            ySpeed += gravity * rb.gravityScale / longJumpMult * Time.deltaTime;
        }
        else if (isGrounded && ySpeed < jumpForce || !isGrounded) 
        {
            ySpeed = rb.velocity.y;
        }

        ySpeed = Mathf.Clamp(ySpeed, -yMaxSpeed, yMaxSpeed);
    }
    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundRadius, groundLayerMask);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundRadius);
    }

}
