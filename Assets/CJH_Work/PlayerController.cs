using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody rb;
    private bool isGround;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 땅에 붙어 있을 때 만 점프하게 구현
        {
            Jump();
        }
    }

    void Move() // wasd 방향키에 따른 움직임 구현
    {
        float moveX = 0f;
        float moveZ = 0f;



        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized; // 방향
        Vector3 moveVelocity = move * moveSpeed;
        Vector3 currentVelocity = rb.velocity;

        rb.velocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);        // 수직 속도(y)는 그대로 유지하고, 수평 이동만 제어
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision) // 땅에 접촉해 있을 때
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision) // 땅에서 떨어졌을 때
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }


}
