using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isRight = true;
    private Vector2 move;
    private float Dame;
    public GameObject coin;

    public float maxHealth;

    public float speed;


    private GameObject Player;

    private State currentState;

    [SerializeField]
    private float groundCheckDis, wallCheckDis, KnockD, TouchDameTime, TouchDame, TouchDameCooldow, TouchDameW, TouchDameH;


    [SerializeField]
    private Transform groundCheck, wallCheck;


    [SerializeField]
    private LayerMask whatGround, whatPlayer;
    [SerializeField]
    private Vector2 KnockSpeed,TouchDameTopL, TouchDameTopR;
    private bool ground, wall;

    private float KnockStarTime;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
/*        Enemy = transform.Find("Slime").gameObject;*/
    }

    private enum State
    {
        Walk,
        Knock,
        Dead,
    }
    


    void Update()
    {
        switch (currentState)
        {
            case State.Walk:
                UpdateWalkState();
                break;
            case State.Knock:
                UpdateKnockState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }

/*        walking();
*/    }
    private void Flip()
    {

        isRight = !isRight;
        transform.Rotate(0, 180, 0);
        speed = -speed;


    }

    //--------Walk State-------------------------
    private void EnterWalkState()
    {

    }
    private void UpdateWalkState()
    {
        // Kiểm tra xem enemy có đang trên mặt đất và không va chạm với tường
        ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatGround);
        wall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDis, whatGround);

        if (!ground || wall)
        {
            Flip();
        }
        else
        {
            // Kiểm tra xem player có tồn tại hay không
            if (Player != null)
            {
                // Lấy vị trí của player
                Vector2 targetPosition = Player.transform.position;
                // Kiểm tra nếu player ở trên cao hơn enemy
                if (targetPosition.y > rb.position.y)
                {
                    move.Set(speed, rb.velocity.y);
                    rb.velocity = move;
                }
                else
                {
                    float direction = targetPosition.x - rb.position.x;

                    // Kiểm tra hướng để quay mặt về phía player
                    if ((direction > 0 && !isRight) || (direction < 0 && isRight))
                    {
                        Flip(); // Quay mặt về phía player
                    }
                    move.Set(speed, rb.velocity.y);
                    rb.velocity = move;
                    if (!ground || wall)
                    {
                        Flip(); // Đảo hướng nếu gặp tường hoặc rơi ra khỏi nền
                    }
                    /* // Di chuyển enemy về phía player chỉ theo trục x
                     Vector2 newPosition = new Vector2(Mathf.MoveTowards(rb.position.x, targetPosition.x, speed * Time.deltaTime),rb.position.y);

                     // Cập nhật vị trí Rigidbody2D
 *//*                    rb.MovePosition(newPosition);*//*
 */
                }
            }
            else
            {
                // Nếu không có player, tiếp tục di chuyển theo hướng hiện tại
                move.Set(speed, rb.velocity.y);
                rb.velocity = move;
            }
        }
    }

    private void ExitWalkState()
    {

    }

    //---------Knock State-----------------------
    private void EnterKnockState()
    {
        KnockStarTime = Time.time;
        move.Set((speed > 0) ? -10 : (speed < 0 ? 10 : KnockSpeed.x), 5);
        rb.velocity = move;
        anim.SetTrigger("Knock");
    }
    private void UpdateKnockState()
    {

        SwitchState(State.Walk);

    }
    private void ExitKnockState()
    {
        StartCoroutine(ExitKnockCoroutine());
    }

    private IEnumerator ExitKnockCoroutine()
    {
        yield return new WaitForSeconds(0.2f); // Thời gian chờ, ví dụ: 0.2 giây
        anim.SetBool("Knock", false);
    }


    //---------Dead State-------------------------
    private void EnterDeadState()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }


    //-----
    
    public void Damage()
    {
        if(maxHealth > 0)
        {
            SwitchState(State.Knock);

        }
        else if(maxHealth <= 0)
        {
            SwitchState(State.Dead);
        }
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walk:
                ExitWalkState();
                break;
            case State.Knock:
                ExitKnockState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walk:
                EnterWalkState();
                break;
            case State.Knock:
                EnterKnockState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDis));

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDis, wallCheck.position.y));
    }

}
