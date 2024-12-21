using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float moveInput;

    public Rigidbody2D rb;
    private Animator anim;

    public int amountOfJump = 2;

    private int amountOfJumpLeft;

    private bool isRight = true;
    private bool isMove;
    private bool isGround;
    private bool canJump;
    private bool canFlip;
    private bool isTouchingWall;
    private bool isWallSliding;

    public float speed = 5f;
    public float jumpp = 11f;
    public float groundcheckRadius;
    public float wallpoin;
    public float wallSlideSpeed = 1f;


    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatGround;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpLeft = amountOfJump;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMove();
        UpdateAnimation();
        CheckJump();
        checkwallsliding();
        EnableFlip();
        DisableFlip();
    }

    private void FixedUpdate()
    {
        Applymove();
        Checkground();
    }

    private void checkwallsliding()
    {
        if(isTouchingWall && !isGround && rb.velocity.y< 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Checkground()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundcheckRadius, whatGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallpoin, whatGround);
    }

    private void CheckJump()
    {
        if (isGround)
        {
            amountOfJumpLeft = amountOfJump;
        }
        if(amountOfJumpLeft < 2)
        { 
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void CheckMove()
    {
        if(isRight && moveInput < 0 )
        {
            Flip();

        }
        else if (!isRight && moveInput > 0)
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isMove = true;
        }
        else {
            isMove = false;
        }
    }

    private void UpdateAnimation ()
    {
        anim.SetBool("is moving", isMove);
    }

    private void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpp);
            amountOfJumpLeft--;
        }
    }
    private void Applymove() 
    {
        
        rb.velocity = new Vector2(speed*moveInput,rb.velocity.y);
        

        if(isWallSliding )
        {
            amountOfJumpLeft = 2;
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }   
    }


    private void DisableFlip()
    {
        canFlip = false;
    }

    private void EnableFlip()
    {
        canFlip = true;
    }

    private void Flip()
    {

            isRight = !isRight;
            transform.Rotate(0,180,0);

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundcheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallpoin, wallCheck.position.y, wallCheck.position.z));
    }
}
