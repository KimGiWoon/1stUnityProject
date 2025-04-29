using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private Rigidbody rb;
    private bool isGround;
    [SerializeField] float fallMultiplier = 2.5f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate() // ★ 물리 기반은 FixedUpdate로!
    {
        Move();
    }


    void Move() // wasd 방향키에 따른 움직임 구현
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 front = new Vector3(moveX, 0f, moveZ).normalized; // 입력 방향을 벡터로 만들어줌. 카메라 방향에 따라 될 수 있음.

        if (front != Vector3.zero) // 움직임이 0이 아니면
        {
            Vector3 camForward = Camera.main.transform.forward; // 카메라 앞 방향
            Vector3 camRight = Camera.main.transform.right; // 카메라 오른쪽 방향

            camForward.y = 0f; // 카메라 y축 고정
            camRight.y = 0f; // 카메라 y축 고정
            camForward.Normalize(); // 벡터는 고정하고 방향만
            camRight.Normalize(); // 벡터는 고정하고 방향만

            Vector3 moveDir = camForward * front.z + camRight * front.x; // 입력값을 카메라 방향 기준으로 변환


            rb.AddForce(moveDir * moveSpeed, ForceMode.Force);
        }
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
