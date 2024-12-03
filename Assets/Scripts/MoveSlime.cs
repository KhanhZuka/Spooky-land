using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour
{
    public float Speed = 2f;          // Tốc độ di chuyển của Slime
    public float detectionRange = 4f; // Khoảng cách phát hiện Player
    public int attackDamage = 10;     // Sát thương khi tấn công Player
    public Rigidbody2D rb;
    public Animator anime;

    private Transform player;         // Vị trí của Player
    private Vector3 moveDirection;    // Hướng di chuyển của Slime
    private bool isChasingPlayer = false;
    private float detectionRangeSqr;  // Bình phương của phạm vi phát hiện

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();
        InvokeRepeating("SetRandomDirection", 2f, 2f); // Thay đổi hướng mỗi 2 giây
        detectionRangeSqr = detectionRange * detectionRange; // Tính bình phương phạm vi phát hiện
    }

    private void Update()
    {
        if (!isChasingPlayer)
        {
            MoveSlime();
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    // Di chuyển ngẫu nhiên khi không đuổi theo Player
    private void MoveSlime()
    {
        transform.Translate(moveDirection * Speed * Time.deltaTime);
    }

    // Đặt hướng di chuyển ngẫu nhiên lên, xuống, trái, phải
    private void SetRandomDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                moveDirection = Vector3.up;
                break;
            case 1:
                moveDirection = Vector3.down;
                break;
            case 2:
                moveDirection = Vector3.left;
                break;
            case 3:
                moveDirection = Vector3.right;
                break;
        }
    }

    // Kiểm tra nếu Player ở trong phạm vi phát hiện để đuổi theo
    private void CheckForPlayer()
    {
        // Sử dụng so sánh bình phương khoảng cách
        if ((transform.position - player.position).sqrMagnitude <= detectionRangeSqr)
        {
            Debug.Log("Slime phát hiện Player và bắt đầu đuổi theo");
            isChasingPlayer = true;
            anime.SetBool("attack", true);
        }
        else
        {
            anime.SetBool("attack", false);
        }
    }

    // Đuổi theo Player
    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Speed * Time.deltaTime);

        // Sử dụng so sánh bình phương khoảng cách khi gần Player
        if ((transform.position - player.position).sqrMagnitude < 1f * 1f)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("Slime tấn công Player, gây " + attackDamage + " sát thương!");
    }
}