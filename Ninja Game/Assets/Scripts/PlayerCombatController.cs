using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnable;
    [SerializeField]
    private float attack1Radius, attack1Dame;
    [SerializeField]
    private Transform attack1HitBox;
    [SerializeField]
    private LayerMask Whatdame;
    private float Dame = 10;

    private bool gotInput, isAttacking, isFirstAttack;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("can attack", combatEnable);
    }

    private void Update()
    {
        CheckCombatInput();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnable)
            {
                gotInput = true;
                CheckAttack();
            }
        }
    }

    private void CheckAttack()
    {
        // Kiểm tra nếu có input và không đang tấn công
        if (gotInput && !isAttacking)
        {
            gotInput = false; // Reset input để tránh kích hoạt lại
            isAttacking = true; // Đặt trạng thái tấn công
            isFirstAttack = !isFirstAttack;

            // Kích hoạt animation
/*            anim.SetBool("is attacking", true);
*/            anim.SetTrigger("is attacking");
            /*            anim.SetBool("first attack", isFirstAttack);
            */
            // Gọi hàm kết thúc tấn công sau thời gian ngắn (giả sử thời gian animation là 0.5s)
            Invoke(nameof(FinishAttack), 0.45f);
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attack1HitBox.position, attack1Radius, Whatdame);

        foreach (Collider2D obj in enemy)
        {
            EnemyController enemyController = obj.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.maxHealth -= Dame;
                enemyController.Damage(); // Gọi thêm hàm Damage()
            }
        }
    }

   
    private void FinishAttack()
    {
        isAttacking = false; // Reset trạng thái tấn công
        anim.SetBool("is attacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
    }
}
