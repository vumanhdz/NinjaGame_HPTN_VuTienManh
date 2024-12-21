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

    public float maxHealth;

    public float speed = 1.5f;


    public GameObject Enemy;

    private State currentState;

    [SerializeField]
    private float groundCheckDis, wallCheckDis, KnockD;


    [SerializeField]
    private Transform groundCheck, wallCheck;


    [SerializeField]
    private LayerMask whatGround;
    [SerializeField]
    private Vector2 KnockSpeed;
    private bool ground, wall;

    private float KnockStarTime;

    // Start is called before the first frame update

    void Start()
    {
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
        ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatGround);
        wall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDis,whatGround);
        if(!ground || wall)
        {
            Flip();
        }
        else
        {
            move.Set(speed, rb.velocity.y);
            rb.velocity = move;
        }

    }
    private void ExitWalkState()
    {

    }

    //---------Knock State-----------------------
    private void EnterKnockState()
    {
        KnockStarTime = Time.time;
        move.Set(KnockSpeed.x*10, KnockSpeed.y);
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
