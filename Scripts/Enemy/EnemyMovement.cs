using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Horizontal Movement")]
    public float enemyAcceleration = 25;
    public float enemyDeceleration = 30;
    public float xMaxSpeed = 10;
    private bool facingRight;
    public float xSpeed;

    [Header("Vertical Movement")]
    public float jumpForce;
    public float longJumpMult = 1;
    public float yMaxSpeed = 20;
    private float gravity;
    private float ySpeed;

    [Header("Collision Settings")]
    public bool isGrounded;
    public float groundRadius = 0.2f;
    public Transform groundCheckPoint;
    public LayerMask groundLayerMask;

    [Header("Inputs")]
    public float enemyDirectionInput;
    private bool enemyJumpInput;
    private bool longJumpButton;

    private void Start()
    {
        enemyDirectionInput = 1;
        facingRight = true;

        gravity = Physics2D.gravity.y;
    }

    private void Update()
    {
        GroundCheck();
        Movement();
    }
    public void EnemyInputManager(int direction, bool jump)
    {
        enemyDirectionInput = direction;
        //enemyJumpInput = jump;
    }
    public void ChangeSpeed(float newTargetSpeed)
    {
        xMaxSpeed = newTargetSpeed;
    }
    private void Movement() 
    {
        HorizontalMovement();
        VerticalMovement();

        rb.velocity = new Vector2(xSpeed, ySpeed);

    }
    private void HorizontalMovement()
    {
        // horizontalInput = xAxis
        if (Mathf.Abs(enemyDirectionInput) > 0)
        {
            Direction();
            xSpeed += enemyDirectionInput * enemyAcceleration * Time.deltaTime;
            xSpeed = Mathf.Clamp(xSpeed, -xMaxSpeed, xMaxSpeed);
        }
        else
        {
            xSpeed = Mathf.MoveTowards(xSpeed, 0, enemyAcceleration * Time.deltaTime);
        }
    }
    private void Direction()
    {
        if (facingRight && enemyDirectionInput < 0)
        {
            Flip();
        }
        if (!facingRight && enemyDirectionInput > 0)
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
    private void VerticalMovement()
    {
        if (isGrounded && enemyJumpInput)
        {
            ySpeed = jumpForce;
        }
        else if (longJumpButton && ySpeed > 0 && !isGrounded)
        {
            ySpeed += gravity * rb.gravityScale / longJumpMult * Time.deltaTime;
        }
        else 
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
