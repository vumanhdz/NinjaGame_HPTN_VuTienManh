using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // Tham chiếu đến Slider
    public float maxHealth = 100f; // Máu tối đa của người chơi
    public float currentHealth;
    public GameObject Enemy;

    public float damageOnCollision = 10f; // Lượng máu mất khi va chạm với enemy

    public LayerMask Enemyy;

    void Start()
    {
        currentHealth = maxHealth; // Đặt máu ban đầu bằng giá trị tối đa
        healthSlider.maxValue = maxHealth; // Thiết lập giá trị tối đa cho Slider
        healthSlider.value = currentHealth; // Đồng bộ Slider với máu hiện tại
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Trừ máu
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giới hạn giá trị máu
        healthSlider.value = currentHealth; // Cập nhật Slider

        if (currentHealth <= 0)
        {
            Die(); // Gọi hàm xử lý khi người chơi chết
        }
    }

    private void Die()
    {
        Debug.Log("Player is dead!");
        // Thực hiện các hành động khi người chơi chết, ví dụ: hiện bảng game over
    }
}
