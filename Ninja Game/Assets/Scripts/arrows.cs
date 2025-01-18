using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Arows : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public Rigidbody2D rb;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        float Deg = 30f * Mathf.Deg2Rad;
        if (Player.transform.rotation.y < 0)
        {
            speed = -speed;
            Deg = -30f * Mathf.Deg2Rad;
        }

        // Tính toán vận tốc theo góc bắn
        float vx = speed * Mathf.Cos(Deg);
        float vy = speed * Mathf.Sin(Deg);

        // Gán vận tốc ban đầu cho Rigidbody2D
        rb.velocity = new Vector2(vx, vy);
    }
    void Update()
    {
        // Xoay mũi tên theo hướng chuyển động
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
}
