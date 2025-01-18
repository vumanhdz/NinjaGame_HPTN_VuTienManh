using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnable;
    [SerializeField]
    private float attack1Radius, attack1Dame, Dgsize;
    [SerializeField]
    private Transform attack1HitBox, Dgzone;
    [SerializeField]
    private LayerMask Whatdame;
    private float Dame = 10;

    public GameObject UiGameOver;
    public GameObject Arrow;
    public float coinConut;
    public TMP_Text coinText;

    private bool gotInput, isAttacking, isFirstAttack, Hurt, isAttacking2, ar;

    private Animator anim;



    public Slider healthSlider; // Tham chiếu đến Slider
    public float maxHealth = 100f; // Máu tối đa của người chơi
    public float currentHealth;
    public float damage = 10f;



    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("can attack", combatEnable);
        currentHealth = maxHealth; // Đặt máu ban đầu bằng giá trị tối đa
        healthSlider.maxValue = maxHealth; // Thiết lập giá trị tối đa cho Slider
        healthSlider.value = currentHealth; // Đồng bộ Slider với máu hiện tại
        InvokeRepeating(nameof(CheckEnemyAttack), 0f, 1f);

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
                CheckAttack1();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (combatEnable)
            {
                gotInput = true;
                CheckAttack2();
            }
        }
    }

    private void CheckAttack1()
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
            // Gọi hàm kết thúc tấn công sau thời gian ngắn (giả sử thời gian animation là 0.5s)
            Invoke(nameof(FinishAttack), 0.45f);
        }
    }
    private void CheckAttack2()
    {
        // Kiểm tra nếu có input và không đang tấn công
        if (gotInput && !isAttacking2)
        {
            gotInput = false; // Reset input để tránh kích hoạt lại
            isAttacking2 = true; // Đặt trạng thái tấn công
            isFirstAttack = !isFirstAttack;

            // Kích hoạt animation
            /*            anim.SetBool("is attacking", true);
            */
            anim.SetTrigger("is attacking2");
            Invoke(nameof(ShootArrow), 0.4f);
            // Gọi hàm kết thúc tấn công sau thời gian ngắn (giả sử thời gian animation là 0.5s)
            Invoke(nameof(FinishAttack), 0.7f);
        }
    }

    private void ShootArrow()
    {
        Instantiate(Arrow, transform.position, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinConut++;
        }
        coinText.text = coinConut.ToString();
/*        if (collision.CompareTag("Enemy"))
        {
            ar = true;
        }
        else
        {
            ar = false;
        }*/
    }

    private void CheckEnemyAttack()
    {
        Collider2D[] enemyy = Physics2D.OverlapCircleAll(Dgzone.position, Dgsize, Whatdame);

        if (enemyy.Length > 0) // Nếu có enemy trong vùng va chạm
        {

            foreach (Collider2D obj in enemyy)
            {
                Hurt = true;
                anim.SetBool("Hurt", Hurt);
                currentHealth -= damage; // Trừ máu
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giới hạn giá trị máu
                healthSlider.value = currentHealth; // Cập nhật Slider

                if (currentHealth <= 0)
                {
                    Die();
                    return;
                }
            }
        }
        else
        {
            Hurt = false;
            anim.SetBool("Hurt", Hurt);
        }
    }


    private void Die()
    {
        UiGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    private void FinishAttack()
    {
        isAttacking = false; // Reset trạng thái tấn công
        anim.SetBool("is attacking", false);
        isAttacking2 = false; // Reset trạng thái tấn công
        anim.SetBool("is attacking2", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBox.position, attack1Radius);
        Gizmos.DrawWireSphere(Dgzone.position, Dgsize);
/*        Gizmos.DrawLine(CoinCheck.position, new Vector2(CoinCheck.position.x + CoinkDis, CoinCheck.position.y));
*/    }
}
