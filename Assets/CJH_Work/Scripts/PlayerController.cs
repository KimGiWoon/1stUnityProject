using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float fallMultiplier = 2.5f;
    private bool isGround;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // 땅에 붙어 있을 때 점프하게 구현
        {
            Jump();
        }

        else
        {
            animator.SetBool("Jump", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;

        if (inputDir != Vector3.zero)
        {
            Vector3 camForward = Camera.main.transform.forward; // 카메라 기준 방향 계산
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x; // 입력값을 카메라 기준 방향에 맞춰 변환

            rb.AddForce(moveDir * moveSpeed, ForceMode.Force); // 이동

            Quaternion targetRotation = Quaternion.LookRotation(moveDir); // 회전 처리
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("Jump", true);
        Debug.Log("isGround는 false");
        isGround = false;
        animator.SetBool("OnGround", false);
    }


    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("isGround는 true");
            isGround = true;
            animator.SetBool("OnGround", true);
        }
    }

}


